using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class BannerResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        public BannerResponse() {}
        public BannerResponse(webecommerce.Data.Banner banner)
        {
            Id = banner.Id;
            ImageUrl = banner.ImageUrl;
            Status = banner.Status;
        }
    }
} 