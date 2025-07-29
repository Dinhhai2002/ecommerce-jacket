using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class SizeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public SizeResponse() {}
        public SizeResponse(webecommerce.Data.Size size)
        {
            Id = size.Id;
            Name = size.Name;
            Code = size.Code;
            Status = size.Status;
        }
    }
} 