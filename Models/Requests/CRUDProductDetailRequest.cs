using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDProductDetailRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public int SizeId { get; set; }

        [Required]
        public int MaterialId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }
} 