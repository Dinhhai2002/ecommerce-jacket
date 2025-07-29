using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("return_request")]
    public class ReturnRequest : BaseEntity
    {
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("reason")]
        public string Reason { get; set; }
        [Column("note")]
        public string Note { get; set; }
        [Column("reject_reason")]
        public string RejectReason { get; set; }
        [Column("refund_amount")]
        public decimal RefundAmount { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 