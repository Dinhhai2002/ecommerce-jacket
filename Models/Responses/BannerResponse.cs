using Newtonsoft.Json;
using System;

namespace webecommerce.Models.Responses
{
    public class BannerResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public BannerResponse()
        {
        }

        public BannerResponse(Banner banner)
        {
            Id = banner.Id;
            Name = banner.Name;
            ImageUrl = banner.ImageUrl;
            Link = banner.Link;
            Status = banner.Status;
            CreatedAt = banner.CreatedAt;
            UpdatedAt = banner.UpdatedAt;
        }
    }
} 