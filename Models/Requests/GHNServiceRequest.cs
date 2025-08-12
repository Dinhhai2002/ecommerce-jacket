using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class GHNServiceRequest
    {
        [Required]
        public int ShopId { get; set; }

        [Required]
        public int FromDistrictId { get; set; }

        [Required]
        public int ToDistrictId { get; set; }
    }
} 