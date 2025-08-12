using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentId { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        public new int Status { get; set; } = 1;

        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public Category()
        {
            Children = new HashSet<Category>();
            Products = new HashSet<Product>();
            Images = new HashSet<Image>();
        }
    }
} 