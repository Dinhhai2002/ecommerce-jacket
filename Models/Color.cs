using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("Colors")]
    public class Color : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public List<ProductDetail> ProductDetails { get; set; }
    }
} 