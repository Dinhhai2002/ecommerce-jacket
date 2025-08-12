using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace webecommerce.Models.Requests
{
    public class StaffOrderRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ShippingName { get; set; }

        [Required]
        public string ShippingPhone { get; set; }

        [Required]
        public int ShippingWardId { get; set; }

        [Required]
        public string ShippingWardName { get; set; }

        [Required]
        public int ShippingDistrictId { get; set; }

        [Required]
        public string ShippingDistrictName { get; set; }

        [Required]
        public int ShippingCityId { get; set; }

        [Required]
        public string ShippingCityName { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        public int? VoucherId { get; set; }

        [Required]
        public int PaymentMethod { get; set; }

        [Required]
        public List<OrderDetailRequest> OrderDetails { get; set; }

        public string Note { get; set; }
    }

    public class OrderDetailRequest
    {
        [Required]
        public int ProductDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public string Note { get; set; }
    }
} 