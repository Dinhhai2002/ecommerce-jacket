using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class DistrictResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("city_id")]
        public int CityId { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public DistrictResponse() {}
        public DistrictResponse(webecommerce.Data.Districts district)
        {
            Id = district.Id;
            Name = district.Name;
            Code = district.Code;
            CityId = district.CityId;
            Status = district.Status;
        }
    }
} 