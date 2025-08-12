using Newtonsoft.Json;
using System.Collections.Generic;

namespace webecommerce.Models.Responses
{
    public class GHNServiceResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<GHNService> Data { get; set; }
    }

    public class GHNService
    {
        [JsonProperty("service_id")]
        public int ServiceId { get; set; }

        [JsonProperty("service_type_id")]
        public int ServiceTypeId { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
} 