using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("review")]
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("rating")]
        public int Rating { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 