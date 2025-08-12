using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Services
{
    public interface ICategoryService
    {
        Task<StoreProcedureListResult<Category>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetByStatusAsync(int status);
        Task<Category> GetByNameAsync(string name);
        Task<bool> CheckNameExistsAsync(string name);
        Task<StoreProcedureListResult<Category>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<IEnumerable<Category>> GetByIdsWithProductsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Category>> GetRootCategoriesAsync();
        Task<IEnumerable<Category>> GetChildrenAsync(int parentId);
    }
} 