using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public abstract class BaseRequest
    {
        [Required]
        public int Status { get; set; } = 1;
    }
} 