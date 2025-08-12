using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class GHNFeeResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public GHNFeeData Data { get; set; }
    }

    public class GHNFeeData
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("service_fee")]
        public decimal ServiceFee { get; set; }

        [JsonProperty("insurance_fee")]
        public decimal InsuranceFee { get; set; }

        [JsonProperty("expected_delivery_time")]
        public string ExpectedDeliveryTime { get; set; }
    }
} 