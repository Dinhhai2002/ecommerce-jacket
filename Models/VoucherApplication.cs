using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Models
{
    [Table("VoucherApplications")]
    public class VoucherApplication : BaseEntity
    {
        [Required]
        public int VoucherId { get; set; }

        public int? ProductId { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public Voucher Voucher { get; set; }
        public Product Product { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
    }
} 