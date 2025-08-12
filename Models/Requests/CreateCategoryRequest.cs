using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CreateCategoryRequest : BaseRequest
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentId { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }
    }
} 