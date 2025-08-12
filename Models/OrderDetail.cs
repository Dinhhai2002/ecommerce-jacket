using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("OrderDetails")]
    public class OrderDetail : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public string Note { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public List<ReturnRequestDetail> ReturnRequestDetails { get; set; }
    }
} 