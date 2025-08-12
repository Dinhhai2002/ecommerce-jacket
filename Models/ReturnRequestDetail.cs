using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("ReturnRequestDetails")]
    public class ReturnRequestDetail : BaseEntity
    {
        [Required]
        public int ReturnRequestId { get; set; }

        [Required]
        public int OrderDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Note { get; set; }

        public string ImageUrl { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public ReturnRequest ReturnRequest { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public List<Image> Images { get; set; }
    }
} 