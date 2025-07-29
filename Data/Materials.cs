using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("materials")]
    public class Materials : BaseEntity
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