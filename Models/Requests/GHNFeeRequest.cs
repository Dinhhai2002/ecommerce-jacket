using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class GHNFeeRequest
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int FromDistrictId { get; set; }

        [Required]
        public int ToDistrictId { get; set; }

        [Required]
        public int ToWardCode { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Length { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Width { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Height { get; set; }
    }
} 