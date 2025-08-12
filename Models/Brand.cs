using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("Brands")]
    public class Brand : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Logo { get; set; }

        [Required]
        public new int Status { get; set; } = 1;

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public Brand()
        {
            Products = new HashSet<Product>();
            Images = new HashSet<Image>();
        }
    }
} 