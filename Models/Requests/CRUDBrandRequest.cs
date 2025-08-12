using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDBrandRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
} 