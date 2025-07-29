using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Utils;
using System.Collections.Generic;
using System;
using System.Linq;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VoucherController : BaseController
    {
        private readonly IVoucherService _voucherService;
        private readonly IVoucherApplicationService _voucherApplicationService;
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductDetailService _productDetailService;
        private readonly IProductService _productService;

        public VoucherController(
            IVoucherService voucherService,
            IVoucherApplicationService voucherApplicationService,
            ICartService cartService,
            ICartDetailService cartDetailService,
            IProductDetailService productDetailService,
            IProductService productService)
        {
            _voucherService = voucherService;
            _voucherApplicationService = voucherApplicationService;
            _cartService = cartService;
            _cartDetailService = cartDetailService;
            _productDetailService = productDetailService;
            _productService = productService;
        }

        [HttpGet("best-voucher")]
        public async Task<IActionResult> GetBestVoucher()
        {
            try
            {
                var user = await GetCurrentUser();
                var pagination = new Pagination(20, 0);

                // Get cart
                var cartResult = await _cartService.GetList(user.Id, "", -1, pagination);
                var cart = cartResult.Data.FirstOrDefault();
                if (cart == null)
                    return BadRequestWithMessage("Cart not found");

                // Get cart details
                var cartDetailResult = await _cartDetailService.GetList(cart.Id, -1, "", 1, pagination);
                var cartDetails = cartDetailResult.Data;
                if (!cartDetails.Any())
                    return BadRequestWithMessage("Cart is empty");

                // Create maps
                var cartDetailMap = cartDetails.ToDictionary(cd => cd.ProductDetailId);
                var productDetailIds = cartDetails.Select(cd => cd.ProductDetailId).ToHashSet();
                var productDetails = await _productDetailService.GetByIds(productDetailIds.ToList());
                var productDetailMap = productDetails.ToDictionary(pd => pd.Id);

                // Calculate total amount
                var totalAmount = cartDetails.Sum(cd =>
                {
                    var productDetail = productDetailMap.GetValueOrDefault(cd.ProductDetailId);
                    return productDetail != null ? productDetail.Price * cd.Quantity : 0;
                });

                // Get available vouchers
                var allVouchers = await _voucherService.GetAll();
                var availableVouchers = allVouchers
                    .Where(v => v.IsCurrentDateInRange())
                    .Where(v => !v.IsNumberLimit())
                    .Where(v => totalAmount >= v.MinOrderValue)
                    .ToList();

                decimal bestDiscountAmount = 0;
                Voucher bestVoucher = null;
                VoucherResponse bestVoucherResponse = null;

                foreach (var voucher in availableVouchers)
                {
                    var amountVoucher = 0m;
                    var voucherApplicationResult = await _voucherApplicationService.GetList(
                        voucher.Id, -1, -1, -1, "", 1, pagination);
                    var voucherApplication = voucherApplicationResult.Data.FirstOrDefault();
                    var listProductIdsApplyVoucher = new HashSet<int>();
                    var products = await _productService.GetAll();
                    var productMap = products.ToDictionary(p => p.Id);

                    if (voucherApplication == null || (
                        string.IsNullOrEmpty(voucherApplication.ProductId) &&
                        string.IsNullOrEmpty(voucherApplication.BrandId) &&
                        string.IsNullOrEmpty(voucherApplication.CategoryId)))
                    {
                        amountVoucher = CalculateTotalAmountApplyVoucher(totalAmount, voucher);
                    }
                    else
                    {
                        amountVoucher = await CalculateAmountWithVoucherApplication(
                            cartDetails, productDetails, productDetailMap,
                            voucher, voucherApplication, listProductIdsApplyVoucher, productMap);
                    }

                    if (amountVoucher > bestDiscountAmount && amountVoucher <= totalAmount)
                    {
                        bestDiscountAmount = amountVoucher;
                        bestVoucher = voucher;
                        bestVoucherResponse = bestVoucher != null ? new VoucherResponse { Voucher = bestVoucher } : null;
                    }
                }

                if (bestVoucher != null && bestDiscountAmount > bestVoucher.MaxDiscount)
                {
                    bestDiscountAmount = bestVoucher.MaxDiscount;
                }

                var discountedTotal = totalAmount - bestDiscountAmount;
                var response = new VoucherApplyResponse
                {
                    DiscountedTotal = discountedTotal,
                    DiscountAmount = bestDiscountAmount,
                    Voucher = bestVoucherResponse
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _voucherService.GetList(keySearch, status, pagination);

                var listData = new BaseListDataResponse<VoucherResponse>
                {
                    List = result.Data.Select(v => new VoucherResponse { Voucher = v }).ToList(),
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var voucher = await _voucherService.GetById(id);
                if (voucher == null)
                    return BadRequestWithMessage("Voucher not found");

                return OkWithData(new VoucherResponse { Voucher = voucher });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/change-status")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            try
            {
                var voucher = await _voucherService.GetById(id);
                if (voucher == null)
                    return BadRequestWithMessage("Voucher not found");

                voucher.Status = voucher.Status == 1 ? 0 : 1;
                await _voucherService.Update(voucher);

                return OkWithData(new VoucherResponse { Voucher = voucher });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDVoucherRequest request)
        {
            try
            {
                var existingVoucher = await _voucherService.GetByCode(request.Code);
                if (existingVoucher != null)
                    return BadRequestWithMessage("Voucher already exists");

                var voucher = new Voucher
                {
                    Code = request.Code,
                    DiscountType = request.DiscountType,
                    DiscountValue = request.DiscountValue,
                    MinOrderValue = request.MinOrderValue,
                    MaxDiscount = request.MaxDiscount,
                    StartDate = Utils.ConvertStringToDate(request.StartDate),
                    EndDate = Utils.ConvertStringToDate(request.EndDate),
                    UsageLimit = request.UsageLimit,
                    UsedCount = request.UsedCount,
                    Status = 1
                };

                await _voucherService.Create(voucher);
                return OkWithData(new VoucherResponse { Voucher = voucher });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDVoucherRequest request)
        {
            try
            {
                var voucher = await _voucherService.GetById(id);
                if (voucher == null)
                    return BadRequestWithMessage("Voucher not found");

                if (voucher.Code != request.Code)
                {
                    var existingVoucher = await _voucherService.GetByCode(request.Code);
                    if (existingVoucher != null)
                        return BadRequestWithMessage("Voucher code already exists");
                }

                voucher.Code = request.Code;
                voucher.DiscountType = request.DiscountType;
                voucher.DiscountValue = request.DiscountValue;
                voucher.MinOrderValue = request.MinOrderValue;
                voucher.MaxDiscount = request.MaxDiscount;
                voucher.StartDate = Utils.ConvertStringToDate(request.StartDate);
                voucher.EndDate = Utils.ConvertStringToDate(request.EndDate);
                voucher.UsageLimit = request.UsageLimit;
                voucher.UsedCount = request.UsedCount;

                await _voucherService.Update(voucher);
                return OkWithData(new VoucherResponse { Voucher = voucher });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/apply")]
        public async Task<IActionResult> ApplyVoucher(int id, [FromBody] ApplyVoucherRequest request)
        {
            try
            {
                var user = await GetCurrentUser();
                var voucher = await _voucherService.GetById(id);
                if (voucher == null)
                    return BadRequestWithMessage("Voucher not found");

                var pagination = new Pagination(20, 0);
                var cartResult = await _cartService.GetList(user.Id, "", -1, pagination);
                var cart = cartResult.Data.FirstOrDefault();
                var cartDetailResult = await _cartDetailService.GetList(cart.Id, -1, "", 1, pagination);
                var cartDetails = cartDetailResult.Data;

                var cartDetailMap = cartDetails.ToDictionary(cd => cd.ProductDetailId);
                var productDetailIds = cartDetails.Select(cd => cd.ProductDetailId).ToHashSet();
                var productDetails = await _productDetailService.GetByIds(productDetailIds.ToList());
                var productIds = productDetails.Select(pd => pd.ProductId).ToHashSet();
                var products = await _productService.GetByIds(productIds.ToList());
                var productMap = products.ToDictionary(p => p.Id);

                if (!voucher.IsCurrentDateInRange() || voucher.IsNumberLimit() || request.TotalAmount < voucher.MinOrderValue)
                    return BadRequestWithMessage("Voucher cannot be applied");

                var listProductIdsApplyVoucher = new HashSet<int>();
                decimal amountVoucher = 0;

                var voucherApplicationResult = await _voucherApplicationService.GetList(
                    voucher.Id, -1, -1, -1, "", 1, pagination);
                var voucherApplication = voucherApplicationResult.Data.FirstOrDefault();

                if (voucherApplication == null || (
                    string.IsNullOrEmpty(voucherApplication.ProductId) &&
                    string.IsNullOrEmpty(voucherApplication.BrandId) &&
                    string.IsNullOrEmpty(voucherApplication.CategoryId)))
                {
                    amountVoucher = CalculateTotalAmountApplyVoucher(request.TotalAmount, voucher);
                }
                else
                {
                    amountVoucher = await CalculateAmountWithVoucherApplication(
                        cartDetails, productDetails, productDetailMap,
                        voucher, voucherApplication, listProductIdsApplyVoucher, productMap);
                }

                if (request.TotalAmount < amountVoucher || amountVoucher >= voucher.MaxDiscount)
                {
                    if (voucher.DiscountType == 2 && request.TotalAmount < amountVoucher)
                        amountVoucher = request.TotalAmount;
                    else
                        amountVoucher = voucher.MaxDiscount;
                }

                var totalAmount = request.TotalAmount - amountVoucher;

                if (amountVoucher == 0)
                    return BadRequestWithMessage("Voucher cannot be applied");

                var response = new VoucherApplyResponse
                {
                    DiscountedTotal = totalAmount,
                    DiscountAmount = amountVoucher,
                    Voucher = new VoucherResponse { Voucher = voucher }
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        private decimal CalculateTotalAmountApplyVoucher(decimal amount, Voucher voucher)
        {
            if (voucher.DiscountType == 1) // Percentage
                return amount * (voucher.DiscountValue / 100);
            else // Fixed amount
                return voucher.DiscountValue;
        }

        private async Task<decimal> CalculateAmountWithVoucherApplication(
            List<CartDetail> cartDetails,
            List<ProductDetail> productDetails,
            Dictionary<int, ProductDetail> productDetailMap,
            Voucher voucher,
            VoucherApplication voucherApplication,
            HashSet<int> listProductIdsApplyVoucher,
            Dictionary<int, Product> productMap)
        {
            decimal amountVoucher = 0;

            if (!string.IsNullOrEmpty(voucherApplication.CategoryId))
            {
                foreach (var cartDetail in cartDetails)
                {
                    var productDetail = productDetailMap.GetValueOrDefault(cartDetail.ProductDetailId);
                    if (listProductIdsApplyVoucher.Contains(productDetail.Id))
                        continue;

                    var product = productMap.GetValueOrDefault(productDetail.ProductId);
                    if (voucherApplication.CategoryId == product.CategoryId.ToString())
                    {
                        amountVoucher += CalculateTotalAmountApplyVoucher(productDetail.Price, voucher);
                        listProductIdsApplyVoucher.Add(productDetail.Id);
                    }
                }
            }

            if (!string.IsNullOrEmpty(voucherApplication.BrandId))
            {
                foreach (var cartDetail in cartDetails)
                {
                    var productDetail = productDetailMap.GetValueOrDefault(cartDetail.ProductDetailId);
                    if (listProductIdsApplyVoucher.Contains(productDetail.Id))
                        continue;

                    var product = productMap.GetValueOrDefault(productDetail.ProductId);
                    if (voucherApplication.BrandId == product.BrandId.ToString())
                    {
                        amountVoucher += CalculateTotalAmountApplyVoucher(productDetail.Price, voucher);
                        listProductIdsApplyVoucher.Add(productDetail.Id);
                    }
                }
            }

            if (!string.IsNullOrEmpty(voucherApplication.ProductId))
            {
                foreach (var cartDetail in cartDetails)
                {
                    var productDetail = productDetailMap.GetValueOrDefault(cartDetail.ProductDetailId);
                    if (listProductIdsApplyVoucher.Contains(productDetail.Id))
                        continue;

                    if (voucherApplication.ProductId == productDetail.ProductId.ToString())
                    {
                        amountVoucher += CalculateTotalAmountApplyVoucher(productDetail.Price, voucher);
                        listProductIdsApplyVoucher.Add(productDetail.Id);
                    }
                }
            }

            return amountVoucher;
        }
    }
} 