using System.ComponentModel.DataAnnotations;

namespace webecommerce.Common.Utils
{
    public class Pagination
    {
        [Required]
        public int PageNumber { get; set; } = 1;

        [Required]
        public int PageSize { get; set; } = 10;
    }
} 