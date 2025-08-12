using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class ChangePaymentStatusRequest
    {
        [Required]
        public int PaymentStatus { get; set; }

        public string Note { get; set; }
    }
} 