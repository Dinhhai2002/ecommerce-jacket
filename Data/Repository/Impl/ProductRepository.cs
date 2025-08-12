using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository.Impl
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product> GetBySkuAsync(string sku)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Sku == sku);
        }

        public async Task<bool> CheckSkuExistsAsync(string sku)
        {
            return await _context.Products.AnyAsync(p => p.Sku == sku);
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            return await _context.Products.AnyAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Product>> GetByBrandIdAsync(int brandId)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.BrandId == brandId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<StoreProcedureListResult<Product>> GetListByBrandAsync(int brandId, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.BrandId == brandId && p.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(p => p.Name.Contains(searchKey) || 
                                       p.Sku.Contains(searchKey) || 
                                       p.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Product>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<StoreProcedureListResult<Product>> GetListByCategoryAsync(int categoryId, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.CategoryId == categoryId && p.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(p => p.Name.Contains(searchKey) || 
                                       p.Sku.Contains(searchKey) || 
                                       p.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Product>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<StoreProcedureListResult<Product>> GetListByPriceRangeAsync(decimal minPrice, decimal maxPrice, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(p => p.Name.Contains(searchKey) || 
                                       p.Sku.Contains(searchKey) || 
                                       p.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Product>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public override async Task<StoreProcedureListResult<Product>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.Status == status);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(p => p.Name.Contains(searchKey) || 
                                       p.Sku.Contains(searchKey) || 
                                       p.Description.Contains(searchKey));
            }

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<Product>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }
    }
} 