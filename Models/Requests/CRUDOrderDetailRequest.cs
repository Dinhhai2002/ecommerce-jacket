using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDOrderDetailRequest
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string Note { get; set; }
    }
} 