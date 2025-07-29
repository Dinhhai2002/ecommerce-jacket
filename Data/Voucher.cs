using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("voucher")]
    public class Voucher : BaseEntity
    {
        public int Id { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("value")]
        public decimal Value { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("min_order_value")]
        public decimal MinOrderValue { get; set; }
        [Column("max_discount_amount")]
        public decimal MaxDiscountAmount { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 