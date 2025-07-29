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
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductDetailService _productDetailService;

        public OrderDetailController(
            IOrderDetailService orderDetailService,
            IProductDetailService productDetailService)
        {
            _orderDetailService = orderDetailService;
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int orderId = 0,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _orderDetailService.GetList(orderId, keySearch, status, pagination);

                var listData = new BaseListDataResponse<OrderDetailResponse>
                {
                    List = result.Data.Select(o => new OrderDetailResponse { OrderDetail = o }).ToList(),
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
                var orderDetail = await _orderDetailService.GetById(id);
                if (orderDetail == null)
                    return BadRequestWithMessage("Order detail not found");

                return OkWithData(new OrderDetailResponse { OrderDetail = orderDetail });
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
                var orderDetail = await _orderDetailService.GetById(id);
                if (orderDetail == null)
                    return BadRequestWithMessage("Order detail not found");

                orderDetail.Status = orderDetail.Status == 1 ? 0 : 1;
                await _orderDetailService.Update(orderDetail);

                return OkWithData(new OrderDetailResponse { OrderDetail = orderDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDOrderDetailRequest request)
        {
            try
            {
                var existingOrderDetail = await _orderDetailService.GetByName(request.Name);
                if (existingOrderDetail != null)
                    return BadRequestWithMessage("Order detail with this name already exists");

                var orderDetail = new OrderDetail
                {
                    Name = request.Name,
                    Status = 1
                };

                await _orderDetailService.Create(orderDetail);
                return OkWithData(new OrderDetailResponse { OrderDetail = orderDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDOrderDetailRequest request)
        {
            try
            {
                var orderDetail = await _orderDetailService.GetById(id);
                if (orderDetail == null)
                    return BadRequestWithMessage("Order detail not found");

                await _orderDetailService.Update(orderDetail);
                return OkWithData(new OrderDetailResponse { OrderDetail = orderDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("by-order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            try
            {
                var pagination = new Pagination(100, 0);
                var result = await _orderDetailService.GetList(orderId, "", 1, pagination);

                var productDetailIds = result.Data.Select(o => o.ProductDetailId).ToList();
                var productDetails = await _productDetailService.GetByIds(productDetailIds);
                var productDetailMap = productDetails.ToDictionary(
                    pd => pd.Id,
                    pd => new ProductDetailResponse { ProductDetail = pd }
                );

                var orderDetailResponses = result.Data.Select(orderDetail =>
                {
                    var productDetailResponse = productDetailMap.GetValueOrDefault(orderDetail.ProductDetailId);
                    return new OrderDetailResponse 
                    { 
                        OrderDetail = orderDetail,
                        ProductDetail = productDetailResponse
                    };
                }).ToList();

                var listData = new BaseListDataResponse<OrderDetailResponse>
                {
                    List = orderDetailResponses,
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 