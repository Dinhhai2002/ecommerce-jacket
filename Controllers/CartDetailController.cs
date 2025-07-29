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
    public class CartDetailController : BaseController
    {
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductDetailService _productDetailService;

        public CartDetailController(
            ICartDetailService cartDetailService,
            IProductDetailService productDetailService)
        {
            _cartDetailService = cartDetailService;
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int cartId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _cartDetailService.GetList(cartId, -1, keySearch, status, pagination);

                var productDetailIds = result.Data.Select(cd => cd.ProductDetailId).ToHashSet();
                var productDetails = await _productDetailService.GetByIds(productDetailIds.ToList());
                var productDetailMap = productDetails.ToDictionary(pd => pd.Id, pd => new ProductDetailResponse { ProductDetail = pd });

                var listData = new BaseListDataResponse<CartDetailResponse>
                {
                    List = result.Data.Select(cd => new CartDetailResponse
                    {
                        CartDetail = cd,
                        ProductDetail = productDetailMap.GetValueOrDefault(cd.ProductDetailId)
                    }).ToList(),
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
                var cartDetail = await _cartDetailService.GetById(id);
                if (cartDetail == null)
                    return BadRequestWithMessage("Cart detail not found");

                return OkWithData(new CartDetailResponse { CartDetail = cartDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDCartDetailRequest request)
        {
            try
            {
                var productDetail = await _productDetailService.GetById(request.ProductDetailId);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not found");

                if (request.Quantity > productDetail.Stock)
                    return BadRequestWithMessage("Insufficient quantity");

                var cartDetail = new CartDetail
                {
                    CartId = request.CartId,
                    ProductDetailId = request.ProductDetailId,
                    Quantity = request.Quantity
                };

                await _cartDetailService.Create(cartDetail);

                var response = new CartDetailResponse
                {
                    CartDetail = cartDetail,
                    ProductDetail = new ProductDetailResponse { ProductDetail = productDetail }
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDCartDetailRequest request)
        {
            try
            {
                var cartDetail = await _cartDetailService.GetById(id);
                if (cartDetail == null)
                    return BadRequestWithMessage("Cart detail not found");

                if (request.Quantity == 0)
                {
                    await _cartDetailService.Delete(id);
                    return OkWithMessage("Cart detail deleted successfully");
                }

                var productDetail = await _productDetailService.GetById(cartDetail.ProductDetailId);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not in cart");

                if (request.Quantity > productDetail.Stock)
                    return BadRequestWithMessage("Insufficient quantity");

                cartDetail.Quantity = request.Quantity;
                await _cartDetailService.Update(cartDetail);

                return OkWithData(new CartDetailResponse { CartDetail = cartDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 