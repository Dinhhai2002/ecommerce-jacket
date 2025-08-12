using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "User";

        public DateTime? LastLogin { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Required]
        public new int Status { get; set; } = 1;

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<AddressBook> AddressBooks { get; set; }

        public User()
        {
            Orders = new HashSet<Order>();
            Carts = new HashSet<Cart>();
            Reviews = new HashSet<Review>();
            AddressBooks = new HashSet<AddressBook>();
        }
    }
} 