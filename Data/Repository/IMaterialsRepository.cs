using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IMaterialsRepository : IGenericRepository<Materials>
    {
        Materials FindByCode(string code);
        Materials FindByName(string name);
        List<Materials> FindByStatus(int status);
        List<Materials> FindAllActive();
    }
} 