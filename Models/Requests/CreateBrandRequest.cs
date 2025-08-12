using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CreateBrandRequest : BaseRequest
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Logo { get; set; }
    }
} 