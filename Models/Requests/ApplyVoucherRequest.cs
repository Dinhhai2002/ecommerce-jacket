using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class ApplyVoucherRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public int? ProductId { get; set; }

        public int? CategoryId { get; set; }
    }
} 