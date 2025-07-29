using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public int Id { get; set; }
        [Column("cart_id")]
        public int CartId { get; set; }
        [Column("product_detail_id")]
        public int ProductDetailId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 