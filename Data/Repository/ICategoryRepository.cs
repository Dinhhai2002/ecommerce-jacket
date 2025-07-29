using System.Collections.Generic;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        List<Category> FindByIds(List<int> ids);
        Category FindByName(string name);
        StoreProcedureListResult<Category> SpGListCategory(int parentId, string keySearch, int status, Pagination pagination);
    }
} 