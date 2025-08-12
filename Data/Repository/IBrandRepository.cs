using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<Brand> GetByNameAsync(string name);
        Task<bool> CheckNameExistsAsync(string name);
        Task<StoreProcedureListResult<Brand>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null);
        Task<IEnumerable<Brand>> GetByIdsWithProductsAsync(IEnumerable<int> ids);
    }
} 