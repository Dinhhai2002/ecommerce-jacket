using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("ReturnRequests")]
    public class ReturnRequest : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Reason { get; set; }

        public string Note { get; set; }

        public string ImageUrl { get; set; }

        public decimal RefundAmount { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public User User { get; set; }
        public List<ReturnRequestDetail> ReturnRequestDetails { get; set; }
        public List<ReturnRequestHistory> ReturnRequestHistories { get; set; }
        public List<Image> Images { get; set; }
    }
} 