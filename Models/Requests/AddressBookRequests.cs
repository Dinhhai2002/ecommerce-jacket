using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CreateAddressBookRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public string DistrictName { get; set; }

        [Required]
        public int WardId { get; set; }

        [Required]
        public string WardName { get; set; }

        [Required]
        public string FullAddress { get; set; }

        public bool IsDefault { get; set; }
    }

    public class UpdateAddressBookRequest : CreateAddressBookRequest
    {
        [Required]
        public int Status { get; set; }
    }

    public class SetDefaultAddressRequest
    {
        [Required]
        public int AddressId { get; set; }
    }
} 