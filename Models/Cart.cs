using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("Carts")]
    public class Cart : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public User User { get; set; }
        public List<CartDetail> CartDetails { get; set; }
    }
} 