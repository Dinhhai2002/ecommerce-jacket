using Newtonsoft.Json;
using System;

namespace webecommerce.Models.Requests
{
    public class CRUDVoucherRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }
        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }
        [JsonProperty("min_order_value")]
        public decimal MinOrderValue { get; set; }
        [JsonProperty("max_discount_amount")]
        public decimal MaxDiscountAmount { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
    }
} 