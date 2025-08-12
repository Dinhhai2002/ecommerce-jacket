using System;
using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public abstract class BaseResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
} 