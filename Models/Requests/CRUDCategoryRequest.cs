using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDCategoryRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
} 