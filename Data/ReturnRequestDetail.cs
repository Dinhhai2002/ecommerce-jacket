using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("return_request_detail")]
    public class ReturnRequestDetail : BaseEntity
    {
        public int Id { get; set; }
        [Column("return_request_id")]
        public int ReturnRequestId { get; set; }
        [Column("order_detail_id")]
        public int OrderDetailId { get; set; }
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