using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data // Adjust namespace to match your project
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() {}
    }

    public class User
    {
        public int Id { get; set; }
        [Column("user_name")]
        public string Username { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Column("avatar_id")]
        public int AvatarId { get; set; }
        [Column("avatar_url")]
        public string AvatarUrl { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public int Gender { get; set; }
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
        public int Role { get; set; }
        public int Otp { get; set; }
        [Column("otp_created_at")]
        public DateTime? OtpCreatedAt { get; set; }
        [Column("is_confirm_otp")]
        public int IsConfirmOtp { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
        [Column("is_google")]
        public int IsGoogle { get; set; }
        public User() {}
    }

    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public Cart() {}
    }
}