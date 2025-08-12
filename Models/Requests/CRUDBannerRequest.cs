using System.ComponentModel.DataAnnotations;

namespace webecommerce.Models.Requests
{
    public class CRUDBannerRequest
    {
        [Required]
        public string Name { get; set; }

        public string Link { get; set; }
    }
} 