using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface ICityService
    {
        void Create(Cities city);
        Cities FindOne(int id);
        void Update(Cities city);
        List<Cities> GetAll();
        Cities FindByCode(string code);
        List<Cities> FindByStatus(int status);
        List<Cities> FindAllActive();
    }
} 