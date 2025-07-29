using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("category")]
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 