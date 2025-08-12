using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace webecommerce.Models.Responses
{
    public class ReviewResponse : BaseResponse
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("images")]
        public string Images { get; set; }

        [JsonProperty("user")]
        public UserResponse User { get; set; }

        public static implicit operator ReviewResponse(Review review)
        {
            if (review == null) return null;

            return new ReviewResponse
            {
                Id = review.Id,
                UserId = review.UserId,
                ProductId = review.ProductId,
                Rating = review.Rating,
                Comment = review.Comment,
                Images = review.Images,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt,
                Status = review.Status,
                User = review.User != null ? (UserResponse)review.User : null
            };
        }
    }

    public class ReviewSummaryResponse
    {
        [JsonProperty("averageRating")]
        public double AverageRating { get; set; }

        [JsonProperty("totalReviews")]
        public int TotalReviews { get; set; }

        [JsonProperty("ratingCounts")]
        public Dictionary<int, int> RatingCounts { get; set; }

        public static ReviewSummaryResponse FromReviews(IEnumerable<Review> reviews)
        {
            var ratingCounts = new Dictionary<int, int>();
            for (int i = 1; i <= 5; i++)
            {
                ratingCounts[i] = reviews.Count(r => r.Rating == i);
            }

            return new ReviewSummaryResponse
            {
                AverageRating = reviews.Any() ? Math.Round(reviews.Average(r => r.Rating), 1) : 0,
                TotalReviews = reviews.Count(),
                RatingCounts = ratingCounts
            };
        }
    }
} 