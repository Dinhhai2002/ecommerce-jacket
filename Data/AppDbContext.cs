using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using webecommerce.Models;

namespace webecommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AddressBook> AddressBooks { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ReturnRequest> ReturnRequests { get; set; }
        public DbSet<ReturnRequestDetail> ReturnRequestDetails { get; set; }
        public DbSet<ReturnRequestHistory> ReturnRequestHistories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRegister> UserRegisters { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherApplication> VoucherApplications { get; set; }
        public DbSet<Wards> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Phone).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Sku).IsUnique();
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.HasIndex(e => e.Sku).IsUnique();
                entity.HasIndex(e => e.Barcode).IsUnique();
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });
        }
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