using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("Reviews")]
    public class Review : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
} 