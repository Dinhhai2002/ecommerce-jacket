using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<UserResponse> GetByIdAsync(int id);
        Task<StoreProcedureListResult<User>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<StoreProcedureListResult<User>> GetListByRoleAsync(string role, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<UserResponse> UpdateProfileAsync(int userId, UpdateProfileRequest request);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<bool> RevokeTokenAsync(int userId);
        Task<bool> UpdateStatusAsync(int userId, UpdateUserStatusRequest request);
        Task<bool> UpdateRoleAsync(int userId, UpdateUserRoleRequest request);
        Task<bool> UpdateAvatarAsync(int userId, string avatarUrl);
        Task<bool> DeleteAsync(int userId);
    }
} 