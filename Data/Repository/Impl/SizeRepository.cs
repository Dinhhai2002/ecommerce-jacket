using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        private readonly AppDbContext _context;

        public SizeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StoreProcedureListResult<Size>> SpGListSize(string keySearch, int status, Pagination pagination)
        {
            var query = _context.Sizes.AsQueryable();

            if (!string.IsNullOrEmpty(keySearch))
                query = query.Where(s => s.Name.Contains(keySearch));

            query = query.Where(s => s.Status == status);

            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new StoreProcedureListResult<Size>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<Size> FindByName(string name)
        {
            return await _context.Sizes.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Size> FindByCode(string code)
        {
            return await _context.Sizes.FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<List<Size>> FindByStatus(int status)
        {
            return await _context.Sizes.Where(s => s.Status == status).ToListAsync();
        }

        public async Task<List<Size>> FindAllActive()
        {
            return await _context.Sizes.Where(s => s.Status == 1).ToListAsync();
        }
    }
} 