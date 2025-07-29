using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("product_detail")]
    public class ProductDetail : BaseEntity
    {
        public int Id { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("color_id")]
        public int ColorId { get; set; }
        [Column("size_id")]
        public int SizeId { get; set; }
        [Column("material_id")]
        public int MaterialId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 