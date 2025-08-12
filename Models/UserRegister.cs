using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("UserRegisters")]
    public class UserRegister : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public int Role { get; set; } = 2; // 1: Admin, 2: User

        [Required]
        public new int Status { get; set; } = 1; // 1: Active, 0: Inactive
    }
} 