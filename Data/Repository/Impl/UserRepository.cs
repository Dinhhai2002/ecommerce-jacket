using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> CheckPhoneExistsAsync(string phone)
        {
            return await _dbSet.AnyAsync(u => u.Phone == phone);
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);
        }

        public async Task<StoreProcedureListResult<User>> GetListByRoleAsync(string role, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _dbSet.AsQueryable();

            // Apply role filter
            query = query.Where(u => u.Role == role);

            // Apply status filter if status is not -1 (all)
            if (status != -1)
            {
                query = query.Where(u => u.Status == status);
            }

            // Apply search if searchKey is provided
            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(u => 
                    u.Email.Contains(searchKey) || 
                    u.FullName.Contains(searchKey) || 
                    u.Phone.Contains(searchKey)
                );
            }

            // Get total count
            var totalRecords = await query.CountAsync();

            // Apply pagination if provided
            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            // Execute query
            var items = await query.ToListAsync();

            return new StoreProcedureListResult<User>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<bool> UpdateLastLoginAsync(int userId)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null)
                return false;

            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime? expiryTime)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null)
                return false;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expiryTime;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RevokeRefreshTokenAsync(int userId)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null)
                return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null)
                return false;

            user.PasswordHash = newPasswordHash;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAvatarAsync(int userId, string avatarUrl)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null)
                return false;

            user.Avatar = avatarUrl;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRoleAsync(int userId, string role)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null)
                return false;

            user.Role = role;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 