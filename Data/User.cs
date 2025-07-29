using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace webecommerce.Data
{
    [Table("users")]
    public class User : BaseEntity
    {
        public int Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("avatar_id")]
        public int AvatarId { get; set; }
        [Column("avatar_url")]
        public string AvatarUrl { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("gender")]
        public int Gender { get; set; }
        [Column("birthday")]
        public string Birthday { get; set; }
        [Column("ward_id")]
        public int WardId { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("district_id")]
        public int DistrictId { get; set; }
        [Column("full_address")]
        public string FullAddress { get; set; }
        [Column("access_token")]
        public string AccessToken { get; set; }
        [Column("is_login")]
        public int IsLogin { get; set; }
        [Column("role")]
        public int Role { get; set; }
        [Column("otp")]
        public int Otp { get; set; }
        [Column("otp_created_at")]
        public DateTime OtpCreatedAt { get; set; }
        [Column("is_confirm_otp")]
        public int IsConfirmOtp { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
        [Column("is_google")]
        public int IsGoogle { get; set; }
    }
} 