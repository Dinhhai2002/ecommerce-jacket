using System;
using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class SizeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public static implicit operator SizeResponse(Size size)
        {
            return new SizeResponse
            {
                Id = size.Id,
                Name = size.Name,
                Description = size.Description
            };
        }
    }
} 