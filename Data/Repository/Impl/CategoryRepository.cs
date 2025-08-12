using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository.Impl
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<StoreProcedureListResult<Category>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Products)
                .Include(c => c.Images)
                .Where(c => c.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(c => c.Name.Contains(searchKey) || 
                                       c.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Category>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<IEnumerable<Category>> GetByIdsWithProductsAsync(IEnumerable<int> ids)
        {
            return await _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Products)
                .Include(c => c.Images)
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetRootCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.Children)
                .Include(c => c.Images)
                .Where(c => c.ParentId == null && c.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetChildrenAsync(int parentId)
        {
            return await _context.Categories
                .Include(c => c.Children)
                .Include(c => c.Images)
                .Where(c => c.ParentId == parentId && c.Status == 1)
                .ToListAsync();
        }

        public override async Task<StoreProcedureListResult<Category>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Images)
                .Where(c => c.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(c => c.Name.Contains(searchKey) || 
                                       c.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Category>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }
    }
} 