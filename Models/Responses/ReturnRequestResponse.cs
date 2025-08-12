using Newtonsoft.Json;
using System;
namespace webecommerce.Models.Responses
{
    public class ReturnRequestResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("reject_reason")]
        public string RejectReason { get; set; }
        [JsonProperty("refund_amount")]
        public decimal RefundAmount { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public ReturnRequestResponse() {}
        public ReturnRequestResponse(webecommerce.Data.ReturnRequest request)
        {
            Id = request.Id;
            OrderId = request.OrderId;
            UserId = request.UserId;
            Reason = request.Reason;
            Note = request.Note;
            RejectReason = request.RejectReason;
            RefundAmount = request.RefundAmount;
            Status = request.Status;
        }
    }
} 