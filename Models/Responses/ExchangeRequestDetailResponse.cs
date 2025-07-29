using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ExchangeRequestDetailResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("exchange_request_id")]
        public int ExchangeRequestId { get; set; }
        [JsonProperty("order_detail_id")]
        public int OrderDetailId { get; set; }
        [JsonProperty("product_detail_id")]
        public int ProductDetailId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("exchange_product_detail_id")]
        public int ExchangeProductDetailId { get; set; }
        [JsonProperty("exchange_quantity")]
        public int ExchangeQuantity { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public ExchangeRequestDetailResponse() {}
        public ExchangeRequestDetailResponse(webecommerce.Data.ExchangeRequestDetail detail)
        {
            Id = detail.Id;
            ExchangeRequestId = detail.ExchangeRequestId;
            OrderDetailId = detail.OrderDetailId;
            ProductDetailId = detail.ProductDetailId;
            Quantity = detail.Quantity;
            ExchangeProductDetailId = detail.ExchangeProductDetailId;
            ExchangeQuantity = detail.ExchangeQuantity;
            Status = detail.Status;
        }
    }
} 