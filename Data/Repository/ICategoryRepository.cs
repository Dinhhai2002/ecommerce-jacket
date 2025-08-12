using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
        Task<bool> CheckNameExistsAsync(string name);
        Task<StoreProcedureListResult<Category>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<IEnumerable<Category>> GetByIdsWithProductsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Category>> GetRootCategoriesAsync();
        Task<IEnumerable<Category>> GetChildrenAsync(int parentId);
    }
} 