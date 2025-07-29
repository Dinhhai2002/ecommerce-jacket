using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("districts")]
    public class Districts : BaseEntity
    {
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 