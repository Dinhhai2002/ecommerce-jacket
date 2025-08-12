using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Data.Repository;
using webecommerce.Security;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly JwtConfig _jwtConfig;

        public UserService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IEmailService emailService,
            IOptions<JwtConfig> jwtConfig)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _emailService = emailService;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !VerifyPasswordHash(request.Password, user.Password))
                throw new Exception("Invalid email or password");

            if (user.Status != 1)
                throw new Exception("Account is disabled");

            var accessToken = _jwtService.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateRefreshTokenAsync(user.Id, refreshToken, refreshTokenExpiryTime);
            await _userRepository.UpdateLastLoginAsync(user.Id);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtConfig.ExpirationInMinutes * 60,
                User = user
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.CheckEmailExistsAsync(request.Email))
                throw new Exception("Email already exists");

            if (await _userRepository.CheckPhoneExistsAsync(request.Phone))
                throw new Exception("Phone number already exists");

            var user = new User
            {
                Email = request.Email,
                Password = HashPassword(request.Password),
                FullName = request.FullName,
                Phone = request.Phone,
                Role = "User",
                Status = 1
            };

            user = await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<StoreProcedureListResult<User>> GetListAsync(
            string searchKey = "",
            int status = 1,
            Pagination pagination = null)
        {
            return await _userRepository.GetListAsync(searchKey, status, pagination);
        }

        public async Task<StoreProcedureListResult<User>> GetListByRoleAsync(
            string role,
            string searchKey = "",
            int status = 1,
            Pagination pagination = null)
        {
            return await _userRepository.GetListByRoleAsync(role, searchKey, status, pagination);
        }

        public async Task<UserResponse> UpdateProfileAsync(int userId, UpdateProfileRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            if (user.Phone != request.Phone && await _userRepository.CheckPhoneExistsAsync(request.Phone))
                throw new Exception("Phone number already exists");

            user.FullName = request.FullName;
            user.Phone = request.Phone;
            if (!string.IsNullOrEmpty(request.Avatar))
                user.Avatar = request.Avatar;

            user = await _userRepository.UpdateAsync(user);
            return user;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            if (!VerifyPasswordHash(request.CurrentPassword, user.Password))
                throw new Exception("Current password is incorrect");

            return await _userRepository.UpdatePasswordAsync(userId, HashPassword(request.NewPassword));
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return false;

            var token = GeneratePasswordResetToken();
            // Store token in cache or temporary storage
            
            var resetLink = $"https://your-domain.com/reset-password?token={token}&email={request.Email}";
            await _emailService.SendPasswordResetEmailAsync(request.Email, resetLink);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return false;

            // Verify token from cache or temporary storage

            return await _userRepository.UpdatePasswordAsync(user.Id, HashPassword(request.NewPassword));
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var principal = _jwtService.ValidateToken(request.AccessToken);
            if (principal == null)
                throw new Exception("Invalid access token");

            var userId = int.Parse(principal.FindFirst("sub")?.Value);
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateRefreshTokenAsync(user.Id, newRefreshToken, refreshTokenExpiryTime);

            return new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = _jwtConfig.ExpirationInMinutes * 60
            };
        }

        public async Task<bool> RevokeTokenAsync(int userId)
        {
            return await _userRepository.RevokeRefreshTokenAsync(userId);
        }

        public async Task<bool> UpdateStatusAsync(int userId, UpdateUserStatusRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Status = request.Status;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UpdateRoleAsync(int userId, UpdateUserRoleRequest request)
        {
            return await _userRepository.UpdateRoleAsync(userId, request.Role);
        }

        public async Task<bool> UpdateAvatarAsync(int userId, string avatarUrl)
        {
            return await _userRepository.UpdateAvatarAsync(userId, avatarUrl);
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == storedHash;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GeneratePasswordResetToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
} 