using Newtonsoft.Json;
using System;

namespace webecommerce.Models.Responses
{
    public class OrderDetailResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
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

        public OrderDetailResponse() {}
        public OrderDetailResponse(webecommerce.Data.OrderDetail detail)
        {
            Id = detail.Id;
            OrderId = detail.OrderId;
            ProductDetailId = detail.ProductDetailId;
            Quantity = detail.Quantity;
            Price = detail.Price;
            TotalPrice = detail.TotalPrice;
            Status = detail.Status;
        }
    }
} 