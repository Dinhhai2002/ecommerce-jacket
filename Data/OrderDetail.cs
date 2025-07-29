using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("product_detail_id")]
        public int ProductDetailId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 