using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace webecommerce.Models.Responses
{
    public class BrandResponse : BaseResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("images")]
        public ICollection<ImageResponse> Images { get; set; }

        [JsonProperty("totalProducts")]
        public int TotalProducts => Products?.Count ?? 0;

        [JsonProperty("products")]
        public ICollection<ProductResponse> Products { get; set; }

        public static implicit operator BrandResponse(Brand brand)
        {
            if (brand == null) return null;

            return new BrandResponse
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                Logo = brand.Logo,
                Status = brand.Status,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt,
                Images = brand.Images?.Select(i => (ImageResponse)i).ToList(),
                Products = brand.Products?.Select(p => (ProductResponse)p).ToList()
            };
        }
    }
} 