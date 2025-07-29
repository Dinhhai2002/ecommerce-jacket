using Newtonsoft.Json;

namespace webecommerce.Models.Requests
{
    public class CRUDCartRequest
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
    }
} 