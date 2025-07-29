using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ReviewResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("product_id")]
        public int ProductId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        public ReviewResponse() {}
        public ReviewResponse(webecommerce.Data.Review review)
        {
            Id = review.Id;
            UserId = review.UserId;
            ProductId = review.ProductId;
            Content = review.Content;
            Rating = review.Rating;
            Status = review.Status;
        }
    }
} 