using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository.Impl
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Brand> GetByNameAsync(string name)
        {
            return await _context.Brands
                .Include(b => b.Images)
                .FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            return await _context.Brands.AnyAsync(b => b.Name == name);
        }

        public async Task<StoreProcedureListResult<Brand>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Brands
                .Include(b => b.Products)
                .Include(b => b.Images)
                .Where(b => b.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(b => b.Name.Contains(searchKey) || 
                                       b.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Brand>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<IEnumerable<Brand>> GetByIdsWithProductsAsync(IEnumerable<int> ids)
        {
            return await _context.Brands
                .Include(b => b.Products)
                .Include(b => b.Images)
                .Where(b => ids.Contains(b.Id))
                .ToListAsync();
        }

        public override async Task<StoreProcedureListResult<Brand>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Brands
                .Include(b => b.Images)
                .Where(b => b.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(b => b.Name.Contains(searchKey) || 
                                       b.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Brand>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }
    }
} 