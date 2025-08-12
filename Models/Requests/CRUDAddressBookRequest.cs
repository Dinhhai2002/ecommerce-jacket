using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDAddressBookRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int WardId { get; set; }

        [Required]
        public string WardName { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public string DistrictName { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string FullAddress { get; set; }

        public int IsDefault { get; set; }
    }
} 