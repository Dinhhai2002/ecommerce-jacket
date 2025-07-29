using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class CartDetailResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("cart_id")]
        public int CartId { get; set; }
        [JsonProperty("product_detail_id")]
        public int ProductDetailId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        public CartDetailResponse() {}
        public CartDetailResponse(webecommerce.Data.CartDetail detail)
        {
            Id = detail.Id;
            CartId = detail.CartId;
            ProductDetailId = detail.ProductDetailId;
            Quantity = detail.Quantity;
            Status = detail.Status;
        }
    }
} 