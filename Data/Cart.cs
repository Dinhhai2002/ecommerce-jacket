using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("cart")]
    public class Cart : BaseEntity
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public int Status { get; set; }
    }
} 