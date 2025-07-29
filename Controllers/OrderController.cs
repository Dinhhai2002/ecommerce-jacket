using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Utils;
using webecommerce.Common.Enums;
using System.Collections.Generic;
using System;
using System.Linq;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IVoucherService _voucherService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductDetailService _productDetailService;
        private readonly IAddressBookService _addressBookService;

        public OrderController(
            IOrderService orderService,
            IVoucherService voucherService,
            IOrderDetailService orderDetailService,
            ICartService cartService,
            ICartDetailService cartDetailService,
            IProductDetailService productDetailService,
            IAddressBookService addressBookService)
        {
            _orderService = orderService;
            _voucherService = voucherService;
            _orderDetailService = orderDetailService;
            _cartService = cartService;
            _cartDetailService = cartDetailService;
            _productDetailService = productDetailService;
            _addressBookService = addressBookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int userId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int paymentStatus = -1,
            [FromQuery] int paymentMethod = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _orderService.GetList(userId, keySearch, status, paymentStatus, paymentMethod, pagination);

                var listData = new BaseListDataResponse<OrderResponse>
                {
                    List = result.Data.Select(o => new OrderResponse { Order = o }).ToList(),
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
                var order = await _orderService.GetById(id);
                if (order == null)
                    return BadRequestWithMessage("Order not found");

                var pagination = new Pagination(20, 0);
                var orderDetailResult = await _orderDetailService.GetList(order.Id, "", 1, pagination);
                var orderDetails = orderDetailResult.Data;

                var productDetailIds = orderDetails.Select(od => od.ProductDetailId).ToList();
                var productDetails = await _productDetailService.GetByIds(productDetailIds);
                var productDetailMap = productDetails.ToDictionary(pd => pd.Id, pd => new ProductDetailResponse { ProductDetail = pd });

                var orderDetailResponses = orderDetails.Select(od => new OrderDetailResponse
                {
                    OrderDetail = od,
                    ProductDetail = productDetailMap.GetValueOrDefault(od.ProductDetailId)
                }).ToList();

                VoucherResponse voucherResponse = null;
                if (order.VoucherId > 0)
                {
                    var voucher = await _voucherService.GetById(order.VoucherId);
                    if (voucher != null)
                        voucherResponse = new VoucherResponse { Voucher = voucher };
                }

                var response = new OrderResponse
                {
                    Order = order,
                    OrderDetails = orderDetailResponses,
                    Voucher = voucherResponse
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
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusOrderRequest request)
        {
            try
            {
                var order = await _orderService.GetById(id);
                if (order == null)
                    return BadRequestWithMessage("Order not found");

                if (!Enum.IsDefined(typeof(StatusOrderEnum), request.Status))
                    return BadRequestWithMessage("Invalid order status");

                var currentStatus = order.Status;
                var newStatus = request.Status;

                if (currentStatus == (int)StatusOrderEnum.DELIVERED)
                    return BadRequestWithMessage("Cannot change status of completed order");

                if (currentStatus == (int)StatusOrderEnum.CANCELLED)
                    return BadRequestWithMessage("Cannot change status of cancelled order");

                if (!IsValidStatusTransition(currentStatus, newStatus))
                    return BadRequestWithMessage("Invalid status transition");

                if (newStatus == (int)StatusOrderEnum.DELIVERED)
                {
                    if (order.PaymentMethod == (int)PaymentMethodEnum.COD || order.PaymentMethod == (int)PaymentMethodEnum.STORE)
                    {
                        order.PaymentStatus = (int)PaymentStatusEnum.PAID;
                    }
                    else if (order.PaymentStatus != (int)PaymentStatusEnum.PAID)
                    {
                        return BadRequestWithMessage("Cannot complete unpaid order");
                    }
                }
                else if (newStatus == (int)StatusOrderEnum.CANCELLED)
                {
                    if (order.PaymentStatus == (int)PaymentStatusEnum.PENDING || order.PaymentStatus == (int)PaymentStatusEnum.PROCESSING)
                    {
                        order.PaymentStatus = (int)PaymentStatusEnum.CANCELLED;
                    }

                    if (currentStatus == (int)StatusOrderEnum.CONFIRMED && 
                        (order.PaymentMethod == (int)PaymentMethodEnum.COD || order.PaymentMethod == (int)PaymentMethodEnum.STORE))
                    {
                        try
                        {
                            await RestoreProductStock(order.Id);
                        }
                        catch (Exception ex)
                        {
                            // Log error but still allow cancellation
                            Console.WriteLine($"Error restoring stock: {ex.Message}");
                        }
                    }

                    if (order.PaymentMethod == (int)PaymentMethodEnum.VNPAY && order.Status == (int)StatusOrderEnum.PROCESSING)
                    {
                        try
                        {
                            await RestoreProductStock(order.Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error restoring stock: {ex.Message}");
                        }
                    }
                }
                else if (newStatus == (int)StatusOrderEnum.CONFIRMED)
                {
                    if (order.PaymentMethod == (int)PaymentMethodEnum.COD || order.PaymentMethod == (int)PaymentMethodEnum.STORE)
                    {
                        try
                        {
                            await UpdateProductStock(order.Id);
                        }
                        catch (Exception ex)
                        {
                            return BadRequestWithMessage(ex.Message);
                        }
                    }
                }

                order.Status = newStatus;
                await _orderService.Update(order);

                return OkWithData(new OrderResponse { Order = order });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDOrderRequest request)
        {
            try
            {
                var user = await GetCurrentUser();

                var shippingAddress = await _addressBookService.GetById(request.AddressId);
                if (shippingAddress == null || shippingAddress.UserId != user.Id)
                    return BadRequestWithMessage("Address not found");

                var pagination = new Pagination(1, 0);
                var cartResult = await _cartService.GetList(user.Id, "", 1, pagination);
                var cart = cartResult.Data.FirstOrDefault();
                if (cart == null)
                    return BadRequestWithMessage("Cart not found");

                var cartDetailResult = await _cartDetailService.GetList(cart.Id, -1, "", 1, new Pagination(100, 0));
                var cartDetails = cartDetailResult.Data;
                if (!cartDetails.Any())
                    return BadRequestWithMessage("Cart is empty");

                var productDetailIds = cartDetails.Select(cd => cd.ProductDetailId).ToList();
                var productDetails = await _productDetailService.GetByIds(productDetailIds);
                var productDetailMap = productDetails.ToDictionary(pd => pd.Id);

                foreach (var cartDetail in cartDetails)
                {
                    var productDetail = productDetailMap.GetValueOrDefault(cartDetail.ProductDetailId);
                    if (productDetail != null && cartDetail.Quantity > productDetail.Stock)
                    {
                        return BadRequestWithMessage($"Product {productDetail.Name} exceeds available stock. Available: {productDetail.Stock}");
                    }
                }

                var order = new Order
                {
                    UserId = user.Id,
                    Price = request.Price,
                    DiscountAmount = request.DiscountAmount,
                    TotalPrice = request.TotalPrice,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = (int)PaymentStatusEnum.PENDING,
                    Status = (int)StatusOrderEnum.PENDING,
                    AddressId = shippingAddress.Id,
                    ShippingName = shippingAddress.FullName,
                    ShippingPhone = shippingAddress.Phone,
                    ShippingWardId = shippingAddress.WardId,
                    ShippingWardName = shippingAddress.WardName,
                    ShippingDistrictId = shippingAddress.DistrictId,
                    ShippingDistrictName = shippingAddress.DistrictName,
                    ShippingCityId = shippingAddress.CityId,
                    ShippingCityName = shippingAddress.CityName,
                    ShippingAddress = shippingAddress.FullAddress,
                    AmountShipping = request.AmountShipping
                };

                if (request.VoucherId > 0)
                {
                    var voucher = await _voucherService.GetById(request.VoucherId);
                    if (voucher == null)
                        return BadRequestWithMessage("Voucher not found");

                    if (!voucher.IsCurrentDateInRange() || voucher.IsNumberLimit() || request.TotalPrice < voucher.MinOrderValue)
                        return BadRequestWithMessage("Voucher cannot be applied");

                    voucher.UsedCount++;
                    await _voucherService.Update(voucher);

                    order.VoucherId = request.VoucherId;
                }
                else
                {
                    order.VoucherId = 0;
                }

                await _orderService.Create(order);

                foreach (var cartDetail in cartDetails)
                {
                    var productDetail = productDetailMap[cartDetail.ProductDetailId];
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductDetailId = cartDetail.ProductDetailId,
                        Quantity = cartDetail.Quantity,
                        Price = productDetail.Price,
                        TotalPrice = productDetail.Price * cartDetail.Quantity,
                        Status = 1
                    };

                    await _orderDetailService.Create(orderDetail);
                    await _cartDetailService.Delete(cartDetail.Id);
                }

                if (request.PaymentMethod == (int)PaymentMethodEnum.VNPAY)
                {
                    var paymentUrl = await GenerateVnPayUrl(request.TotalPrice, order.Id.ToString());
                    return OkWithData(paymentUrl);
                }

                return OkWithData(new OrderResponse { Order = order });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("create-by-staff")]
        public async Task<IActionResult> CreateByStaff([FromBody] StaffOrderRequest request)
        {
            try
            {
                var user = await GetCurrentUser();

                var shippingAddress = await _addressBookService.GetById(request.AddressId);
                if (shippingAddress == null || shippingAddress.UserId != user.Id)
                    return BadRequestWithMessage("Address not found");

                var productDetailIds = request.Products.Select(p => p.ProductDetailId).ToList();
                var productDetails = await _productDetailService.GetByIds(productDetailIds);
                var productDetailMap = productDetails.ToDictionary(pd => pd.Id);

                foreach (var item in request.Products)
                {
                    var productDetail = productDetailMap.GetValueOrDefault(item.ProductDetailId);
                    if (productDetail == null || item.Quantity > productDetail.Stock)
                    {
                        return BadRequestWithMessage($"Product {productDetail?.Name ?? "unknown"} exceeds available stock");
                    }
                }

                var order = new Order
                {
                    UserId = user.Id,
                    Price = request.Price,
                    DiscountAmount = request.DiscountAmount,
                    TotalPrice = request.TotalPrice,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = (int)PaymentStatusEnum.PAID,
                    Status = (int)StatusOrderEnum.DELIVERED,
                    CustomerPhone = request.CustomerPhone,
                    AddressId = shippingAddress.Id,
                    ShippingName = shippingAddress.FullName,
                    ShippingPhone = shippingAddress.Phone,
                    ShippingWardId = shippingAddress.WardId,
                    ShippingWardName = shippingAddress.WardName,
                    ShippingDistrictId = shippingAddress.DistrictId,
                    ShippingDistrictName = shippingAddress.DistrictName,
                    ShippingCityId = shippingAddress.CityId,
                    ShippingCityName = shippingAddress.CityName,
                    ShippingAddress = shippingAddress.FullAddress
                };

                await _orderService.Create(order);

                foreach (var item in request.Products)
                {
                    var productDetail = productDetailMap[item.ProductDetailId];
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductDetailId = item.ProductDetailId,
                        Quantity = item.Quantity,
                        Price = productDetail.Price,
                        TotalPrice = productDetail.Price * item.Quantity,
                        Status = 1
                    };

                    await _orderDetailService.Create(orderDetail);
                }

                await UpdateProductStock(order.Id);

                return OkWithData(new OrderResponse { Order = order });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/payment-confirm")]
        public async Task<IActionResult> GetPaymentUrl(int id)
        {
            try
            {
                var user = await GetCurrentUser();
                var order = await _orderService.GetById(id);

                if (order == null)
                    return BadRequestWithMessage("Order not found");

                if (order.UserId != user.Id)
                    return BadRequestWithMessage("You don't have permission to access this order");

                if (order.PaymentStatus != (int)PaymentStatusEnum.PENDING)
                    return BadRequestWithMessage("Order has been paid or cancelled");

                var paymentUrl = await GenerateVnPayUrl(order.TotalPrice, order.Id.ToString());
                return OkWithData(paymentUrl);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/change-payment-status")]
        public async Task<IActionResult> ChangePaymentStatus(int id, [FromBody] ChangePaymentStatusRequest request)
        {
            try
            {
                var order = await _orderService.GetById(id);
                if (order == null)
                    return BadRequestWithMessage("Order not found");

                if (!Enum.IsDefined(typeof(PaymentStatusEnum), request.PaymentStatus))
                    return BadRequestWithMessage("Invalid payment status");

                order.PaymentStatus = request.PaymentStatus;

                if (request.PaymentStatus == (int)PaymentStatusEnum.PAID)
                {
                    order.Status = (int)StatusOrderEnum.PROCESSING;

                    if (order.PaymentMethod == (int)PaymentMethodEnum.VNPAY)
                    {
                        try
                        {
                            await UpdateProductStock(order.Id);
                        }
                        catch (Exception ex)
                        {
                            return BadRequestWithMessage(ex.Message);
                        }
                    }
                }
                else if (request.PaymentStatus == (int)PaymentStatusEnum.FAILED || 
                         request.PaymentStatus == (int)PaymentStatusEnum.CANCELLED)
                {
                    order.Status = (int)StatusOrderEnum.CANCELLED;
                }

                await _orderService.Update(order);

                var orderDetailResult = await _orderDetailService.GetList(order.Id, "", 1, new Pagination(20, 0));
                var orderDetailResponses = orderDetailResult.Data.Select(od => new OrderDetailResponse { OrderDetail = od }).ToList();

                var response = new OrderResponse
                {
                    Order = order,
                    OrderDetails = orderDetailResponses
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                var user = await GetCurrentUser();
                var order = await _orderService.GetById(id);

                if (order == null)
                    return BadRequestWithMessage("Order not found");

                if (order.UserId != user.Id)
                    return BadRequestWithMessage("You don't have permission to cancel this order");

                if (order.Status == (int)StatusOrderEnum.DELIVERED)
                    return BadRequestWithMessage("Cannot cancel completed order");

                if (order.Status == (int)StatusOrderEnum.CANCELLED)
                    return BadRequestWithMessage("Order is already cancelled");

                if (order.Status == (int)StatusOrderEnum.SHIPPED)
                    return BadRequestWithMessage("Cannot cancel order that is being shipped");

                var currentStatus = order.Status;
                order.Status = (int)StatusOrderEnum.CANCELLED;

                if (order.PaymentStatus == (int)PaymentStatusEnum.PENDING || 
                    order.PaymentStatus == (int)PaymentStatusEnum.PROCESSING)
                {
                    order.PaymentStatus = (int)PaymentStatusEnum.CANCELLED;
                }

                if (order.PaymentMethod == (int)PaymentMethodEnum.COD || 
                    order.PaymentMethod == (int)PaymentMethodEnum.STORE)
                {
                    if (currentStatus == (int)StatusOrderEnum.CONFIRMED || 
                        currentStatus == (int)StatusOrderEnum.PROCESSING)
                    {
                        try
                        {
                            await RestoreProductStock(order.Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error restoring stock: {ex.Message}");
                        }
                    }
                }
                else if (order.PaymentMethod == (int)PaymentMethodEnum.VNPAY && 
                         order.PaymentStatus == (int)PaymentStatusEnum.PAID)
                {
                    try
                    {
                        await RestoreProductStock(order.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error restoring stock: {ex.Message}");
                    }
                }

                await _orderService.Update(order);
                return OkWithData(new OrderResponse { Order = order });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("by-payment-status")]
        public async Task<IActionResult> GetByPaymentStatuses(
            [FromQuery] string paymentStatuses = "1,2,3",
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var paymentStatusList = paymentStatuses.Split(',').Select(int.Parse).ToList();
                var result = await _orderService.GetByPaymentStatuses(paymentStatusList, pagination);

                var listData = new BaseListDataResponse<OrderResponse>
                {
                    List = result.Data.Select(o => new OrderResponse { Order = o }).ToList(),
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        private bool IsValidStatusTransition(int currentStatus, int newStatus)
        {
            if (currentStatus == (int)StatusOrderEnum.PENDING)
            {
                return newStatus == (int)StatusOrderEnum.CONFIRMED ||
                       newStatus == (int)StatusOrderEnum.PROCESSING ||
                       newStatus == (int)StatusOrderEnum.CANCELLED;
            }

            if (currentStatus == (int)StatusOrderEnum.CONFIRMED)
            {
                return newStatus == (int)StatusOrderEnum.PROCESSING ||
                       newStatus == (int)StatusOrderEnum.CANCELLED;
            }

            if (currentStatus == (int)StatusOrderEnum.PROCESSING)
            {
                return newStatus == (int)StatusOrderEnum.SHIPPED ||
                       newStatus == (int)StatusOrderEnum.CANCELLED;
            }

            if (currentStatus == (int)StatusOrderEnum.SHIPPED)
            {
                return newStatus == (int)StatusOrderEnum.DELIVERED ||
                       newStatus == (int)StatusOrderEnum.CANCELLED;
            }

            return false;
        }

        private async Task UpdateProductStock(int orderId)
        {
            var pagination = new Pagination(100, 0);
            var orderDetailResult = await _orderDetailService.GetList(orderId, "", 1, pagination);
            var orderDetails = orderDetailResult.Data;

            foreach (var orderDetail in orderDetails)
            {
                var productDetail = await _productDetailService.GetById(orderDetail.ProductDetailId);
                if (productDetail == null)
                    throw new Exception($"Product detail not found with ID: {orderDetail.ProductDetailId}");

                if (productDetail.Stock < orderDetail.Quantity)
                    throw new Exception($"Product {productDetail.Name} has insufficient stock. Available: {productDetail.Stock}");
            }

            foreach (var orderDetail in orderDetails)
            {
                var productDetail = await _productDetailService.GetById(orderDetail.ProductDetailId);
                productDetail.Stock -= orderDetail.Quantity;
                await _productDetailService.Update(productDetail);
            }
        }

        private async Task RestoreProductStock(int orderId)
        {
            var pagination = new Pagination(100, 0);
            var orderDetailResult = await _orderDetailService.GetList(orderId, "", 1, pagination);
            var orderDetails = orderDetailResult.Data;

            foreach (var orderDetail in orderDetails)
            {
                var productDetail = await _productDetailService.GetById(orderDetail.ProductDetailId);
                if (productDetail != null)
                {
                    productDetail.Stock += orderDetail.Quantity;
                    await _productDetailService.Update(productDetail);
                }
            }
        }

        private async Task<string> GenerateVnPayUrl(decimal amount, string orderId)
        {
            // Implementation will depend on your VNPay configuration
            throw new NotImplementedException("VNPay integration not implemented");
        }
    }
} 