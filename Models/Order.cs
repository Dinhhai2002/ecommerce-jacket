using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Required]
        public string OrderCode { get; set; }

        [Required]
        public int UserId { get; set; }

        public int? VoucherId { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal TotalAmount { get; set; }

        public int PaymentMethod { get; set; }

        public int PaymentStatus { get; set; }

        public int Status { get; set; }

        public int? AddressId { get; set; }

        public string ShippingName { get; set; }

        public string ShippingPhone { get; set; }

        public int? ShippingWardId { get; set; }

        public string ShippingWardName { get; set; }

        public int? ShippingDistrictId { get; set; }

        public string ShippingDistrictName { get; set; }

        public int? ShippingCityId { get; set; }

        public string ShippingCityName { get; set; }

        public string ShippingAddress { get; set; }

        public string CustomerPhone { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Voucher Voucher { get; set; }
        public AddressBook Address { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ReturnRequest> ReturnRequests { get; set; }
    }
} 