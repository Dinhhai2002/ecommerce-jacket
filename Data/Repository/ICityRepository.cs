using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface ICityRepository : IGenericRepository<Cities>
    {
        Cities FindByCode(string code);
        List<Cities> FindByStatus(int status);
        List<Cities> FindAllActive();
    }
} 