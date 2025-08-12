using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Services
{
    public interface IProductService
    {
        Task<StoreProcedureListResult<Product>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByStatusAsync(int status);
        Task<Product> GetBySkuAsync(string sku);
        Task<bool> CheckSkuExistsAsync(string sku);
        Task<bool> CheckNameExistsAsync(string name);
        Task<IEnumerable<Product>> GetByBrandIdAsync(int brandId);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<StoreProcedureListResult<Product>> GetListByBrandAsync(int brandId, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<StoreProcedureListResult<Product>> GetListByCategoryAsync(int categoryId, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<StoreProcedureListResult<Product>> GetListByPriceRangeAsync(decimal minPrice, decimal maxPrice, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);
    }
} 