using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("Sizes")]
    public class Size : BaseEntity
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public new int Status { get; set; } = 1;
    }
} 