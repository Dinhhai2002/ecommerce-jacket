using System.Collections.Generic;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        List<Brand> FindByIds(List<int> ids);
        Brand FindByName(string name);
        StoreProcedureListResult<Brand> SpGListBrand(string keySearch, int status, Pagination pagination);
    }
} 