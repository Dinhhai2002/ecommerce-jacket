using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class WardResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("district_id")]
        public int DistrictId { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public WardResponse() {}
        public WardResponse(webecommerce.Data.Wards ward)
        {
            Id = ward.Id;
            Name = ward.Name;
            Code = ward.Code;
            DistrictId = ward.DistrictId;
            Status = ward.Status;
        }
    }
} 