using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("cities")]
    public class Cities : BaseEntity
    {
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 