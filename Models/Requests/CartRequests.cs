using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class AddToCartRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }

    public class UpdateCartItemRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }
    }
} 