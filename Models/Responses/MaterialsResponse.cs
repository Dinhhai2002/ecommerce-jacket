using Newtonsoft.Json;
using System;
namespace webecommerce.Models.Responses
{
    public class MaterialsResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public MaterialsResponse() {}
        public MaterialsResponse(webecommerce.Data.Materials materials)
        {
            Id = materials.Id;
            Name = materials.Name;
            Code = materials.Code;
            Status = materials.Status;
        }
    }
} 