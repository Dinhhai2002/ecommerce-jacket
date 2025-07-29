using Newtonsoft.Json;

namespace webecommerce.Models.Requests
{
    public class CRUDMaterialsRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
    }
} 