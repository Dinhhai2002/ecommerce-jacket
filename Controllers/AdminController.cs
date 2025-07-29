using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Enums;
using System.Collections.Generic;
using System;
using System.Linq;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IProductDetailService _productDetailService;
        private readonly IStatisticalService _statisticalService;

        public AdminController(
            IUserService userService,
            IOrderService orderService,
            IProductDetailService productDetailService,
            IStatisticalService statisticalService)
        {
            _userService = userService;
            _orderService = orderService;
            _productDetailService = productDetailService;
            _statisticalService = statisticalService;
        }

        [Authorize(Roles = "ADMIN,STAFF")]
        [HttpGet("statistical-overview")]
        public async Task<IActionResult> GetWebsiteStats()
        {
            try
            {
                var users = await _userService.GetAll();
                var products = await _productDetailService.GetAll();
                var orders = await _orderService.GetAll();

                var totalUsers = users.Count;
                var totalProducts = products.Count;
                var totalOrders = orders.Count;

                // Calculate total revenue from completed orders
                var totalRevenue = orders
                    .Where(o => o.Status == (int)StatusOrderEnum.DELIVERED)
                    .Sum(o => o.TotalPrice);

                // Calculate daily revenue
                var today = DateTime.Today;
                var dailyRevenue = orders
                    .Where(o => o.Status == (int)StatusOrderEnum.DELIVERED && 
                           o.CreatedAt.Date == today)
                    .Sum(o => o.TotalPrice);

                // Calculate monthly revenue
                var currentMonth = DateTime.Today;
                var monthlyRevenue = orders
                    .Where(o => o.Status == (int)StatusOrderEnum.DELIVERED && 
                           o.CreatedAt.Month == currentMonth.Month && 
                           o.CreatedAt.Year == currentMonth.Year)
                    .Sum(o => o.TotalPrice);

                // Calculate yearly revenue
                var currentYear = DateTime.Today.Year;
                var yearlyRevenue = orders
                    .Where(o => o.Status == (int)StatusOrderEnum.DELIVERED && 
                           o.CreatedAt.Year == currentYear)
                    .Sum(o => o.TotalPrice);

                var response = new WebsiteStatisticalResponse
                {
                    TotalUsers = totalUsers,
                    TotalRevenue = totalRevenue,
                    TotalProducts = totalProducts,
                    TotalOrders = totalOrders,
                    DailyRevenue = dailyRevenue,
                    MonthlyRevenue = monthlyRevenue,
                    YearlyRevenue = yearlyRevenue
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN,STAFF")]
        [HttpGet("amount")]
        public async Task<IActionResult> GetAmount(
            [FromQuery] int numberWeek = 0,
            [FromQuery] string fromDate = "-1",
            [FromQuery] string toDate = "-1",
            [FromQuery] int type = 1)
        {
            try
            {
                var fromDateTime = !string.IsNullOrEmpty(fromDate) && fromDate != "-1" 
                    ? DateTime.Parse(fromDate) 
                    : (DateTime?)null;

                var toDateTime = !string.IsNullOrEmpty(toDate) && toDate != "-1" 
                    ? DateTime.Parse(toDate) 
                    : (DateTime?)null;

                var result = await _statisticalService.GetStatisticalAmount(
                    numberWeek, fromDateTime, toDateTime, type);

                return OkWithData(result);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 