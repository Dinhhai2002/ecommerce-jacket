using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("brand")]
    public class Brand : BaseEntity
    {
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 