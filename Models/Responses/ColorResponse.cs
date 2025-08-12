using Newtonsoft.Json;
using System;
namespace webecommerce.Models.Responses
{
    public class ColorResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public ColorResponse() {}
        public ColorResponse(webecommerce.Data.Color color)
        {
            Id = color.Id;
            Name = color.Name;
            Code = color.Code;
            Status = color.Status;
        }
    }
} 