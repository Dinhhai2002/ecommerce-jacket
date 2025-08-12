using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("Vouchers")]
    public class Voucher : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public decimal Value { get; set; }

        public decimal? MinOrderValue { get; set; }

        public decimal? MaxDiscountValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MaxUsage { get; set; }

        public int UsageCount { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public List<Order> Orders { get; set; }
        public List<VoucherApplication> VoucherApplications { get; set; }
    }
} 