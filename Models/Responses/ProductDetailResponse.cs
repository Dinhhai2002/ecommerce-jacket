using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ProductDetailResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("product_id")]
        public int ProductId { get; set; }
        [JsonProperty("color_id")]
        public int ColorId { get; set; }
        [JsonProperty("size_id")]
        public int SizeId { get; set; }
        [JsonProperty("material_id")]
        public int MaterialId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        public ProductDetailResponse() {}
        public ProductDetailResponse(webecommerce.Data.ProductDetail detail)
        {
            Id = detail.Id;
            ProductId = detail.ProductId;
            ColorId = detail.ColorId;
            SizeId = detail.SizeId;
            MaterialId = detail.MaterialId;
            Quantity = detail.Quantity;
            Price = detail.Price;
            Status = detail.Status;
        }
    }
} 