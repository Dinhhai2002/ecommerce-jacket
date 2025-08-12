using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Services
{
    public interface IBrandService
    {
        Task<StoreProcedureListResult<Brand>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> CreateAsync(Brand brand);
        Task<Brand> UpdateAsync(Brand brand);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<IEnumerable<Brand>> GetByStatusAsync(int status);
        Task<Brand> GetByNameAsync(string name);
        Task<bool> CheckNameExistsAsync(string name);
        Task<StoreProcedureListResult<Brand>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<IEnumerable<Brand>> GetByIdsWithProductsAsync(IEnumerable<int> ids);
    }
} 