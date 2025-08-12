using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;

namespace webecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(
            ICartService cartService,
            IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<CartResponse>> GetMyCart()
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
                return NotFound();

            return Ok((CartResponse)cart);
        }

        [HttpPost("items")]
        public async Task<ActionResult<CartResponse>> AddToCart([FromBody] AddToCartRequest request)
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            // Check if product exists and is available
            var product = await _productService.GetByIdAsync(request.ProductId);
            if (product == null)
                return BadRequest("Product not found");

            if (product.Status != 1)
                return BadRequest("Product is not available");

            // Get or create cart
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                cart = await _cartService.CreateAsync(cart);
            }

            // Add item to cart
            var cartDetail = new CartDetail
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _cartService.AddItemAsync(cartDetail);

            // Return updated cart
            cart = await _cartService.GetCartAsync(userId);
            return Ok((CartResponse)cart);
        }

        [HttpPut("items/{productId}")]
        public async Task<ActionResult<CartResponse>> UpdateCartItem(
            int productId,
            [FromBody] UpdateCartItemRequest request)
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            // Check if product exists
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
                return BadRequest("Product not found");

            // Get cart
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
                return NotFound();

            // Update item quantity
            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == productId);
            if (cartDetail == null)
                return BadRequest("Product not found in cart");

            if (request.Quantity <= 0)
            {
                // Remove item if quantity is 0 or negative
                await _cartService.RemoveItemAsync(cart.Id, productId);
            }
            else
            {
                cartDetail.Quantity = request.Quantity;
                await _cartService.UpdateItemAsync(cartDetail);
            }

            // Return updated cart
            cart = await _cartService.GetCartAsync(userId);
            return Ok((CartResponse)cart);
        }

        [HttpDelete("items/{productId}")]
        public async Task<ActionResult<CartResponse>> RemoveFromCart(int productId)
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            // Get cart
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
                return NotFound();

            // Remove item
            await _cartService.RemoveItemAsync(cart.Id, productId);

            // Return updated cart
            cart = await _cartService.GetCartAsync(userId);
            return Ok((CartResponse)cart);
        }

        [HttpDelete]
        public async Task<ActionResult> ClearCart()
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<ActionResult<CartCountResponse>> GetCartItemCount()
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            var count = await _cartService.GetCartItemCountAsync(userId);
            return Ok(new CartCountResponse { Count = count });
        }

        [HttpGet("total")]
        public async Task<ActionResult<CartTotalResponse>> GetCartTotal()
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirst("sub")?.Value);

            var total = await _cartService.GetCartTotalAsync(userId);
            return Ok(new CartTotalResponse { Total = total });
        }
    }
} 