using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IDistrictService
    {
        void Create(Districts district);
        Districts FindOne(int id);
        void Update(Districts district);
        List<Districts> GetAll();
        Districts FindByCode(string code);
        List<Districts> FindByCityId(int cityId);
        List<Districts> FindByStatus(int status);
        List<Districts> FindAllActive();
    }
} 