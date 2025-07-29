using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ReturnRequestDetailResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("return_request_id")]
        public int ReturnRequestId { get; set; }
        [JsonProperty("order_detail_id")]
        public int OrderDetailId { get; set; }
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

        public ReturnRequestDetailResponse() {}
        public ReturnRequestDetailResponse(webecommerce.Data.ReturnRequestDetail detail)
        {
            Id = detail.Id;
            ReturnRequestId = detail.ReturnRequestId;
            OrderDetailId = detail.OrderDetailId;
            ProductDetailId = detail.ProductDetailId;
            Quantity = detail.Quantity;
            Price = detail.Price;
            TotalPrice = detail.TotalPrice;
            Status = detail.Status;
        }
    }
} 