using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("Images")]
    public class Image : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Alt { get; set; }

        public string Type { get; set; }

        public int? ProductId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? ReviewId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ProductDetailId")]
        public virtual ProductDetail ProductDetail { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; }

        [Required]
        public new int Status { get; set; }
    }
} 