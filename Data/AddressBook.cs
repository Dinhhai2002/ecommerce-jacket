using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("address_book")]
    public class AddressBook : BaseEntity
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("ward_id")]
        public int WardId { get; set; }
        [Column("ward_name")]
        public string WardName { get; set; }
        [Column("district_id")]
        public int DistrictId { get; set; }
        [Column("district_name")]
        public string DistrictName { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("city_name")]
        public string CityName { get; set; }
        [Column("full_address")]
        public string FullAddress { get; set; }
        [Column("is_default")]
        public int IsDefault { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
} 