using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public interface ISizeRepository : IGenericRepository<Size>
    {
        Task<StoreProcedureListResult<Size>> SpGListSize(string keySearch, int status, Pagination pagination);
        Task<Size> FindByName(string name);
        Task<Size> FindByCode(string code);
        Task<List<Size>> FindByStatus(int status);
        Task<List<Size>> FindAllActive();
    }
} 