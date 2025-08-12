using System.Collections.Generic;
using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ProductResponse : BaseResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("discountPrice")]
        public decimal? DiscountPrice { get; set; }

        [JsonProperty("brandId")]
        public int BrandId { get; set; }

        [JsonProperty("brandName")]
        public string BrandName { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("averageRating")]
        public decimal? AverageRating { get; set; }

        [JsonProperty("images")]
        public ICollection<ImageResponse> Images { get; set; }

        [JsonProperty("productDetails")]
        public ICollection<ProductDetailResponse> ProductDetails { get; set; }

        public static implicit operator ProductResponse(Product product)
        {
            if (product == null) return null;

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                BrandId = product.BrandId,
                BrandName = product.Brand?.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                AverageRating = product.AverageRating,
                Status = product.Status,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Images = product.Images?.Select(i => (ImageResponse)i).ToList(),
                ProductDetails = product.ProductDetails?.Select(pd => (ProductDetailResponse)pd).ToList()
            };
        }
    }
} 