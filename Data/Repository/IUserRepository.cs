using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckPhoneExistsAsync(string phone);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task<StoreProcedureListResult<User>> GetListByRoleAsync(string role, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<bool> UpdateLastLoginAsync(int userId);
        Task<bool> UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime? expiryTime);
        Task<bool> RevokeRefreshTokenAsync(int userId);
        Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash);
        Task<bool> UpdateAvatarAsync(int userId, string avatarUrl);
        Task<bool> UpdateRoleAsync(int userId, string role);
    }
} 