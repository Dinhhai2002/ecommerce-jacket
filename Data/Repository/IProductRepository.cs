using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetBySkuAsync(string sku);
        Task<bool> CheckSkuExistsAsync(string sku);
        Task<bool> CheckNameExistsAsync(string name);
        Task<IEnumerable<Product>> GetByBrandIdAsync(int brandId);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<StoreProcedureListResult<Product>> GetListByBrandAsync(int brandId, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<StoreProcedureListResult<Product>> GetListByCategoryAsync(int categoryId, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<StoreProcedureListResult<Product>> GetListByPriceRangeAsync(decimal minPrice, decimal maxPrice, string searchKey = "", int status = 1, Pagination pagination = null);
    }
} 