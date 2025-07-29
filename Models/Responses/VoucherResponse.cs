using Newtonsoft.Json;
using System;

namespace webecommerce.Models.Responses
{
    public class VoucherResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
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

        public VoucherResponse() {}
        public VoucherResponse(webecommerce.Data.Voucher voucher)
        {
            Id = voucher.Id;
            Code = voucher.Code;
            Name = voucher.Name;
            Description = voucher.Description;
            Type = voucher.Type;
            Value = voucher.Value;
            Quantity = voucher.Quantity;
            StartDate = voucher.StartDate;
            EndDate = voucher.EndDate;
            MinOrderValue = voucher.MinOrderValue;
            MaxDiscountAmount = voucher.MaxDiscountAmount;
            Status = voucher.Status;
        }
    }
} 