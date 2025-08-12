using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("CartDetails")]
    public class CartDetail : BaseEntity
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public Cart Cart { get; set; }
        public ProductDetail ProductDetail { get; set; }
    }
} 