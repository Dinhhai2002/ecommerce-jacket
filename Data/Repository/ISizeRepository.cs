using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface ISizeRepository : IGenericRepository<Size>
    {
        Size FindByCode(string code);
        Size FindByName(string name);
        List<Size> FindByStatus(int status);
        List<Size> FindAllActive();
    }
} 