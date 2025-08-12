using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("ProductDetails")]
    public class ProductDetail : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ColorId { get; set; }

        public string Color { get; set; }

        [Required]
        public int SizeId { get; set; }

        public string Size { get; set; }

        [Required]
        public int MaterialId { get; set; }

        public string Material { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string Sku { get; set; }

        public string Barcode { get; set; }

        public string ImageUrl { get; set; }

        public int BrandId { get; set; }

        public string Brand { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Color ColorNavigation { get; set; }
        public Size SizeNavigation { get; set; }
        public Materials MaterialNavigation { get; set; }
        public Brand BrandNavigation { get; set; }
        public Category CategoryNavigation { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Image> Images { get; set; }
    }
} 