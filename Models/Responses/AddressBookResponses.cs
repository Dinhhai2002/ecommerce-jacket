using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class AddressBookResponse : BaseResponse
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("cityId")]
        public int CityId { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("districtId")]
        public int DistrictId { get; set; }

        [JsonProperty("districtName")]
        public string DistrictName { get; set; }

        [JsonProperty("wardId")]
        public int WardId { get; set; }

        [JsonProperty("wardName")]
        public string WardName { get; set; }

        [JsonProperty("fullAddress")]
        public string FullAddress { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }


        public static implicit operator AddressBookResponse(AddressBook address)
        {
            if (address == null) return null;

            return new AddressBookResponse
            {
                Id = address.Id,
                UserId = address.UserId,
                FullName = address.FullName,
                Phone = address.Phone,
                CityId = address.CityId,
                CityName = address.CityName,
                DistrictId = address.DistrictId,
                DistrictName = address.DistrictName,
                WardId = address.WardId,
                WardName = address.WardName,
                FullAddress = address.FullAddress,
                IsDefault = address.IsDefault,
                CreatedAt = address.CreatedAt,
                UpdatedAt = address.UpdatedAt,
                Status = address.Status,
            };
        }
    }
} 