using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class BrandResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        public BrandResponse() {}
        public BrandResponse(webecommerce.Data.Brand brand)
        {
            Id = brand.Id;
            Name = brand.Name;
            Status = brand.Status;
        }
    }
} 