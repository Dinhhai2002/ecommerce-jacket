using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class ApproveReturnRequestRequest
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal RefundAmount { get; set; }

        public string Note { get; set; }
    }
} 