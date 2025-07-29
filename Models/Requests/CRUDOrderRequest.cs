using Newtonsoft.Json;

namespace webecommerce.Models.Requests
{
    public class CRUDOrderRequest
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("voucher_id")]
        public int? VoucherId { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }
        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }
        [JsonProperty("payment_method")]
        public int PaymentMethod { get; set; }
        [JsonProperty("payment_status")]
        public int PaymentStatus { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("address_id")]
        public int? AddressId { get; set; }
        [JsonProperty("shipping_name")]
        public string ShippingName { get; set; }
        [JsonProperty("shipping_phone")]
        public string ShippingPhone { get; set; }
        [JsonProperty("shipping_ward_id")]
        public int? ShippingWardId { get; set; }
        [JsonProperty("shipping_ward_name")]
        public string ShippingWardName { get; set; }
        [JsonProperty("shipping_district_id")]
        public int? ShippingDistrictId { get; set; }
        [JsonProperty("shipping_district_name")]
        public string ShippingDistrictName { get; set; }
        [JsonProperty("shipping_city_id")]
        public int? ShippingCityId { get; set; }
        [JsonProperty("shipping_city_name")]
        public string ShippingCityName { get; set; }
        [JsonProperty("shipping_address")]
        public string ShippingAddress { get; set; }
        [JsonProperty("customer_phone")]
        public string CustomerPhone { get; set; }
        [JsonProperty("amount_shipping")]
        public decimal AmountShipping { get; set; }
    }
} 