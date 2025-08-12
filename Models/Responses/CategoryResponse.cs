using System.Collections.Generic;
using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class CategoryResponse : BaseResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parentId")]
        public int? ParentId { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("parent")]
        public CategoryResponse Parent { get; set; }

        [JsonProperty("children")]
        public ICollection<CategoryResponse> Children { get; set; }

        [JsonProperty("images")]
        public ICollection<ImageResponse> Images { get; set; }

        [JsonProperty("totalProducts")]
        public int TotalProducts => Products?.Count ?? 0;

        [JsonProperty("products")]
        public ICollection<ProductResponse> Products { get; set; }

        public static implicit operator CategoryResponse(Category category)
        {
            if (category == null) return null;

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentId = category.ParentId,
                ImageUrl = category.ImageUrl,
                Status = category.Status,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                Parent = category.Parent,
                Children = category.Children?.Select(c => (CategoryResponse)c).ToList(),
                Images = category.Images?.Select(i => (ImageResponse)i).ToList(),
                Products = category.Products?.Select(p => (ProductResponse)p).ToList()
            };
        }
    }
} 