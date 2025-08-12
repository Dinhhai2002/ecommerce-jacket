using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace webecommerce.Models.Requests
{
    public class ReturnRequestRequest
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Reason { get; set; }

        public string Note { get; set; }

        [Required]
        public List<ReturnRequestDetailRequest> Details { get; set; }
    }

    public class ReturnRequestDetailRequest
    {
        [Required]
        public int OrderDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public string Note { get; set; }
    }
} 