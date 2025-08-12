using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace webecommerce.Models
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Sku { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public decimal? AverageRating { get; set; }

        [Required]
        public new int Status { get; set; } = 1;

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            OrderDetails = new HashSet<OrderDetail>();
            CartDetails = new HashSet<CartDetail>();
        }
    }
} 