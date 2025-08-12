using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDCartDetailRequest
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
} 