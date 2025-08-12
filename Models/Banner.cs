using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("Banners")]
    public class Banner : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Link { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public List<Image> Images { get; set; }
    }
} 