using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class UserRegisterRepository : GenericRepository<UserRegister>, IUserRegisterRepository
    {
        private readonly AppDbContext _context;

        public UserRegisterRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StoreProcedureListResult<UserRegister>> SpGListUserRegister(string keySearch, int status, Pagination pagination)
        {
            var query = _context.UserRegisters.AsQueryable();

            if (!string.IsNullOrEmpty(keySearch))
                query = query.Where(u => u.Username.Contains(keySearch) || u.Email.Contains(keySearch) || u.Phone.Contains(keySearch));

            query = query.Where(u => u.Status == status);

            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new StoreProcedureListResult<UserRegister>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<UserRegister> FindByUsername(string username)
        {
            return await _context.UserRegisters.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserRegister> FindByEmail(string email)
        {
            return await _context.UserRegisters.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserRegister> FindByPhone(string phone)
        {
            return await _context.UserRegisters.FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<List<UserRegister>> FindByStatus(int status)
        {
            return await _context.UserRegisters.Where(u => u.Status == status).ToListAsync();
        }

        public async Task<List<UserRegister>> FindAllActive()
        {
            return await _context.UserRegisters.Where(u => u.Status == 1).ToListAsync();
        }
    }
} 