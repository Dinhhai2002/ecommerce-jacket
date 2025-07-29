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
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public ReviewController(
            IReviewService reviewService,
            IUserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int userId = -1,
            [FromQuery] int productId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _reviewService.GetList(userId, productId, keySearch, status, pagination);

                // Get unique user IDs from reviews
                var userIds = result.Data.Select(r => r.UserId).Distinct().ToList();

                // Get users information
                var users = await _userService.GetByIds(userIds);
                var userMap = users.ToDictionary(u => u.Id);

                var listData = new BaseListDataResponse<ReviewResponse>
                {
                    List = result.Data.Select(r => new ReviewResponse 
                    { 
                        Review = r,
                        User = userMap.GetValueOrDefault(r.UserId)
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
                var review = await _reviewService.GetById(id);
                if (review == null)
                    return BadRequestWithMessage("Review not found");

                var user = await _userService.GetById(review.UserId);
                return OkWithData(new ReviewResponse { Review = review, User = user });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDReviewRequest request)
        {
            try
            {
                var user = await GetCurrentUser();

                // Check if user has already reviewed this product
                var existingReview = await _reviewService.GetByUserIdAndProductId(user.Id, request.ProductId);
                if (existingReview != null)
                    return BadRequestWithMessage("You have already reviewed this product");

                var review = new Review
                {
                    UserId = user.Id,
                    ProductId = request.ProductId,
                    Rating = request.Rating,
                    Comment = request.Comment,
                    Status = 1
                };

                await _reviewService.Create(review);
                return OkWithData(new ReviewResponse { Review = review });
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
                var review = await _reviewService.GetById(id);
                if (review == null)
                    return BadRequestWithMessage("Review not found");

                review.Status = review.Status == 1 ? 0 : 1;
                await _reviewService.Update(review);

                return OkWithData(new ReviewResponse { Review = review });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 