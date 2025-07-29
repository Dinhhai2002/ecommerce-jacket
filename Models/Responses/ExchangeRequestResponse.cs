using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ExchangeRequestResponse
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
        [JsonProperty("status")]
        public int Status { get; set; }

        public ExchangeRequestResponse() {}
        public ExchangeRequestResponse(webecommerce.Data.ExchangeRequest request)
        {
            Id = request.Id;
            OrderId = request.OrderId;
            UserId = request.UserId;
            Reason = request.Reason;
            Note = request.Note;
            RejectReason = request.RejectReason;
            Status = request.Status;
        }
    }
} 