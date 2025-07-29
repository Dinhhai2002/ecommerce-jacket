using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace webecommerce.Data
{
    [Table("order")]
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("voucher_id")]
        public int? VoucherId { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        [Column("payment_method")]
        public int PaymentMethod { get; set; }
        [Column("payment_status")]
        public int PaymentStatus { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("address_id")]
        public int? AddressId { get; set; }
        [Column("shipping_name")]
        public string ShippingName { get; set; }
        [Column("shipping_phone")]
        public string ShippingPhone { get; set; }
        [Column("shipping_ward_id")]
        public int? ShippingWardId { get; set; }
        [Column("shipping_ward_name")]
        public string ShippingWardName { get; set; }
        [Column("shipping_district_id")]
        public int? ShippingDistrictId { get; set; }
        [Column("shipping_district_name")]
        public string ShippingDistrictName { get; set; }
        [Column("shipping_city_id")]
        public int? ShippingCityId { get; set; }
        [Column("shipping_city_name")]
        public string ShippingCityName { get; set; }
        [Column("shipping_address")]
        public string ShippingAddress { get; set; }
        [Column("customer_phone")]
        public string CustomerPhone { get; set; }
        [Column("amount_shipping")]
        public decimal AmountShipping { get; set; }
    }
} 