using Newtonsoft.Json;
using System;

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

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("product_detail")]
        public ProductDetailResponse ProductDetail { get; set; }

        public CartDetailResponse()
        {
        }

        public CartDetailResponse(CartDetail cartDetail)
        {
            Id = cartDetail.Id;
            CartId = cartDetail.CartId;
            ProductDetailId = cartDetail.ProductDetailId;
            Quantity = cartDetail.Quantity;
            Price = cartDetail.Price;
            TotalPrice = cartDetail.TotalPrice;
            Status = cartDetail.Status;
            CreatedAt = cartDetail.CreatedAt;
            UpdatedAt = cartDetail.UpdatedAt;
        }

        public CartDetailResponse(CartDetail cartDetail, ProductDetailResponse productDetail) : this(cartDetail)
        {
            ProductDetail = productDetail;
        }
    }
} 