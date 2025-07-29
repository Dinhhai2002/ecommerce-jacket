using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class CityResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public CityResponse() {}
        public CityResponse(webecommerce.Data.Cities city)
        {
            Id = city.Id;
            Name = city.Name;
            Code = city.Code;
            Status = city.Status;
        }
    }
} 