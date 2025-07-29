using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class AddressBookResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("ward_id")]
        public int WardId { get; set; }
        [JsonProperty("ward_name")]
        public string WardName { get; set; }
        [JsonProperty("district_id")]
        public int DistrictId { get; set; }
        [JsonProperty("district_name")]
        public string DistrictName { get; set; }
        [JsonProperty("city_id")]
        public int CityId { get; set; }
        [JsonProperty("city_name")]
        public string CityName { get; set; }
        [JsonProperty("full_address")]
        public string FullAddress { get; set; }
        [JsonProperty("is_default")]
        public int IsDefault { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }

        public AddressBookResponse() {}
        public AddressBookResponse(webecommerce.Data.AddressBook address)
        {
            Id = address.Id;
            UserId = address.UserId;
            FullName = address.FullName;
            Phone = address.Phone;
            WardId = address.WardId;
            WardName = address.WardName;
            DistrictId = address.DistrictId;
            DistrictName = address.DistrictName;
            CityId = address.CityId;
            CityName = address.CityName;
            FullAddress = address.FullAddress;
            IsDefault = address.IsDefault;
            Status = address.Status;
        }
    }
} 