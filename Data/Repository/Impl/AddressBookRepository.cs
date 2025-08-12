using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class AddressBookRepository : GenericRepository<AddressBook>, IAddressBookRepository
    {
        public AddressBookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<StoreProcedureListResult<AddressBook>> GetListByUserAsync(
            int userId,
            string searchKey = "",
            int status = 1,
            Pagination pagination = null)
        {
            var query = _context.AddressBooks
                .Include(a => a.City)
                .Include(a => a.District)
                .Include(a => a.Ward)
                .Where(a => a.UserId == userId);

            if (!string.IsNullOrEmpty(searchKey))
                query = query.Where(a =>
                    a.FullName.Contains(searchKey) ||
                    a.Phone.Contains(searchKey) ||
                    a.FullAddress.Contains(searchKey));

            if (status != -1)
                query = query.Where(a => a.Status == status);

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<AddressBook>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<AddressBook> GetDefaultAddressAsync(int userId)
        {
            return await _context.AddressBooks
                .Include(a => a.City)
                .Include(a => a.District)
                .Include(a => a.Ward)
                .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault);
        }

        public async Task<bool> SetDefaultAddressAsync(int userId, int addressId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Unset current default address
                var currentDefault = await _context.AddressBooks
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault);

                if (currentDefault != null)
                {
                    currentDefault.IsDefault = false;
                    currentDefault.UpdatedAt = DateTime.UtcNow;
                }

                // Set new default address
                var newDefault = await _context.AddressBooks
                    .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId);

                if (newDefault == null)
                    return false;

                newDefault.IsDefault = true;
                newDefault.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UnsetDefaultAddressAsync(int userId)
        {
            var currentDefault = await _context.AddressBooks
                .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault);

            if (currentDefault == null)
                return false;

            currentDefault.IsDefault = false;
            currentDefault.UpdatedAt = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CheckAddressExistsAsync(int userId, string fullAddress)
        {
            return await _context.AddressBooks
                .AnyAsync(a => a.UserId == userId && a.FullAddress == fullAddress);
        }

        public async Task<bool> CheckUserOwnsAddressAsync(int userId, int addressId)
        {
            return await _context.AddressBooks
                .AnyAsync(a => a.Id == addressId && a.UserId == userId);
        }

        public async Task<IEnumerable<AddressBook>> GetByUserIdAsync(int userId)
        {
            return await _context.AddressBooks
                .Include(a => a.City)
                .Include(a => a.District)
                .Include(a => a.Ward)
                .Where(a => a.UserId == userId && a.Status == 1)
                .ToListAsync();
        }
    }
} 