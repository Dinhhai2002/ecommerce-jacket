using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class ChangeStatusOrderRequest
    {
        [Required]
        public int Status { get; set; }

        public string Note { get; set; }
    }
} 