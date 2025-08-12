using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class RejectReturnRequestRequest
    {
        [Required]
        public string Reason { get; set; }

        public string Note { get; set; }
    }
} 