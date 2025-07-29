using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IMaterialsService
    {
        void Create(Materials materials);
        Materials FindOne(int id);
        void Update(Materials materials);
        List<Materials> GetAll();
        Materials FindByCode(string code);
        Materials FindByName(string name);
        List<Materials> FindByStatus(int status);
        List<Materials> FindAllActive();
    }
} 