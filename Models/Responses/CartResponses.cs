using System.Collections.Generic;
using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class CartResponse : BaseResponse
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("items")]
        public List<CartItemResponse> Items { get; set; }

        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        public static implicit operator CartResponse(Cart cart)
        {
            if (cart == null) return null;

            return new CartResponse
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cart.CartDetails?.Select(cd => (CartItemResponse)cd).ToList(),
                TotalItems = cart.CartDetails?.Sum(cd => cd.Quantity) ?? 0,
                TotalAmount = cart.CartDetails?.Sum(cd => cd.Product.Price * cd.Quantity) ?? 0,
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt,
                Status = cart.Status
            };
        }
    }

    public class CartItemResponse : BaseResponse
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        public static implicit operator CartItemResponse(CartDetail cartDetail)
        {
            if (cartDetail == null) return null;

            return new CartItemResponse
            {
                Id = cartDetail.Id,
                ProductId = cartDetail.ProductId,
                Name = cartDetail.Product?.Name,
                Price = cartDetail.Product?.Price ?? 0,
                Quantity = cartDetail.Quantity,
                Image = cartDetail.Product?.Image,
                CreatedAt = cartDetail.CreatedAt,
                UpdatedAt = cartDetail.UpdatedAt,
                Status = cartDetail.Status
            };
        }
    }

    public class CartCountResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class CartTotalResponse
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }
    }
} 