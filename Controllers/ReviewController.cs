using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Common.Utils;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;

namespace webecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;

        public ReviewController(
            IReviewService reviewService,
            IProductService productService)
        {
            _reviewService = reviewService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<StoreProcedureListResult<ReviewResponse>>> GetList(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _reviewService.GetListAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<ReviewResponse>
            {
                Items = result.Items.Select(r => (ReviewResponse)r).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("by-product/{productId}")]
        public async Task<ActionResult<StoreProcedureListResult<ReviewResponse>>> GetListByProduct(
            int productId,
            [FromQuery] int rating = 0,
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _reviewService.GetListByProductAsync(productId, rating, status, pagination);

            return Ok(new StoreProcedureListResult<ReviewResponse>
            {
                Items = result.Items.Select(r => (ReviewResponse)r).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<StoreProcedureListResult<ReviewResponse>>> GetListByUser(
            int userId,
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _reviewService.GetListByUserAsync(userId, status, pagination);

            return Ok(new StoreProcedureListResult<ReviewResponse>
            {
                Items = result.Items.Select(r => (ReviewResponse)r).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponse>> GetById(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok((ReviewResponse)review);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewResponse>> Create([FromBody] CreateReviewRequest request)
        {
            var product = await _productService.GetByIdAsync(request.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            // Check if user has already reviewed this product
            if (await _reviewService.HasUserReviewedProductAsync(userId, request.ProductId))
                return BadRequest("You have already reviewed this product");

            var review = new Review
            {
                ProductId = request.ProductId,
                UserId = userId,
                Rating = request.Rating,
                Comment = request.Comment,
                Status = request.Status
            };

            review = await _reviewService.CreateAsync(review);

            // Update product average rating
            await _productService.UpdateAverageRatingAsync(request.ProductId);

            return CreatedAtAction(nameof(GetById), new { id = review.Id }, (ReviewResponse)review);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ReviewResponse>> Update(int id, [FromBody] UpdateReviewRequest request)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            // Check if the review belongs to the current user
            if (review.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            
            // Only admin can update status
            if (User.IsInRole("Admin"))
                review.Status = request.Status;

            review = await _reviewService.UpdateAsync(review);

            // Update product average rating
            await _productService.UpdateAverageRatingAsync(review.ProductId);

            return Ok((ReviewResponse)review);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            // Check if the review belongs to the current user
            if (review.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            var result = await _reviewService.DeleteAsync(id);
            if (!result)
                return NotFound();

            // Update product average rating
            await _productService.UpdateAverageRatingAsync(review.ProductId);

            return NoContent();
        }

        [HttpGet("summary/by-product/{productId}")]
        public async Task<ActionResult<ReviewSummaryResponse>> GetReviewSummary(int productId)
        {
            var summary = await _reviewService.GetReviewSummaryAsync(productId);
            return Ok(summary);
        }

        [HttpGet("helpful")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetMostHelpfulReviews(
            [FromQuery] int limit = 10)
        {
            var reviews = await _reviewService.GetMostHelpfulReviewsAsync(limit);
            return Ok(reviews.Select(r => (ReviewResponse)r));
        }

        [HttpPost("{id}/helpful")]
        [Authorize]
        public async Task<ActionResult> MarkReviewAsHelpful(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            var result = await _reviewService.MarkReviewAsHelpfulAsync(id, userId);
            if (!result)
                return BadRequest("You have already marked this review as helpful");

            return NoContent();
        }

        [HttpDelete("{id}/helpful")]
        [Authorize]
        public async Task<ActionResult> UnmarkReviewAsHelpful(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            var result = await _reviewService.UnmarkReviewAsHelpfulAsync(id, userId);
            if (!result)
                return BadRequest("You have not marked this review as helpful");

            return NoContent();
        }
    }
} 