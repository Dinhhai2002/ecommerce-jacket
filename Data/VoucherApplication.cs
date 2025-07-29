using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("voucher_application")]
    public class VoucherApplication : BaseEntity
    {
        public int Id { get; set; }
        [Column("voucher_id")]
        public int VoucherId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 