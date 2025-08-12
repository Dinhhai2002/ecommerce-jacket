using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDReviewRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }
    }
} 