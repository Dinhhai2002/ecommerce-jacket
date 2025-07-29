using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("product")]
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 