using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("exchange_request_detail")]
    public class ExchangeRequestDetail : BaseEntity
    {
        public int Id { get; set; }
        [Column("exchange_request_id")]
        public int ExchangeRequestId { get; set; }
        [Column("order_detail_id")]
        public int OrderDetailId { get; set; }
        [Column("product_detail_id")]
        public int ProductDetailId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("exchange_product_detail_id")]
        public int ExchangeProductDetailId { get; set; }
        [Column("exchange_quantity")]
        public int ExchangeQuantity { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 