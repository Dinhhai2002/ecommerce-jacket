using Newtonsoft.Json;
using System;
namespace webecommerce.Models.Responses
{
    public class VoucherApplicationResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("voucher_id")]
        public int VoucherId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        [JsonProperty("discount_amount")]
        public decimal DiscountAmount { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public VoucherApplicationResponse() {}
        public VoucherApplicationResponse(webecommerce.Data.VoucherApplication application)
        {
            Id = application.Id;
            VoucherId = application.VoucherId;
            UserId = application.UserId;
            OrderId = application.OrderId;
            DiscountAmount = application.DiscountAmount;
            Status = application.Status;
        }
    }
} 