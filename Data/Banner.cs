using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("banner")]
    public class Banner : BaseEntity
    {
        public int Id { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 