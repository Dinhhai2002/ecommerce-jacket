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
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductDetailService _productDetailService;

        public CartController(
            ICartService cartService,
            ICartDetailService cartDetailService,
            IProductDetailService productDetailService)
        {
            _cartService = cartService;
            _cartDetailService = cartDetailService;
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int userId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var user = await GetCurrentUser();
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _cartService.GetList(user.Id, keySearch, status, pagination);

                var listData = new BaseListDataResponse<CartResponse>
                {
                    List = result.Data.Select(c => new CartResponse { Cart = c }).ToList(),
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
                var cart = await _cartService.GetById(id);
                if (cart == null)
                    return BadRequestWithMessage("Cart not found");

                var pagination = new Pagination(20, 0);
                var cartDetailResult = await _cartDetailService.GetList(id, -1, "", 1, pagination);
                var cartDetails = cartDetailResult.Data;

                var productDetailIds = cartDetails.Select(cd => cd.ProductDetailId).ToList();
                var productDetails = await _productDetailService.GetByIds(productDetailIds);
                var productDetailMap = productDetails.ToDictionary(pd => pd.Id);

                var cartDetailResponses = cartDetails.Select(cd =>
                {
                    var productDetail = productDetailMap.GetValueOrDefault(cd.ProductDetailId);
                    return new CartDetailResponse
                    {
                        CartDetail = cd,
                        ProductDetail = productDetail != null ? new ProductDetailResponse { ProductDetail = productDetail } : null
                    };
                }).ToList();

                var response = new CartResponse
                {
                    Cart = cart,
                    CartDetails = cartDetailResponses
                };

                return OkWithData(response);
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
                var cart = await _cartService.GetById(id);
                if (cart == null)
                    return BadRequestWithMessage("Cart not found");

                cart.Status = cart.Status == 1 ? 0 : 1;
                await _cartService.Update(cart);

                return OkWithData(new CartResponse { Cart = cart });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDCartRequest request)
        {
            try
            {
                var user = await GetCurrentUser();
                var pagination = new Pagination(20, 0);
                var existingCarts = await _cartService.GetList(user.Id, "", 1, pagination);

                if (existingCarts.Data.Any())
                    return BadRequestWithMessage("Cart already exists");

                var cart = new Cart
                {
                    UserId = user.Id,
                    Status = 1
                };

                await _cartService.Create(cart);
                return OkWithData(new CartResponse { Cart = cart });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDCartRequest request)
        {
            try
            {
                var user = await GetCurrentUser();
                var cart = await _cartService.GetById(id);
                if (cart == null)
                    return BadRequestWithMessage("Cart not found");

                cart.UserId = user.Id;
                await _cartService.Update(cart);

                return OkWithData(new CartResponse { Cart = cart });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 