using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IWardService
    {
        void Create(Wards ward);
        Wards FindOne(int id);
        void Update(Wards ward);
        List<Wards> GetAll();
        Wards FindByCode(string code);
        List<Wards> FindByDistrictId(int districtId);
        List<Wards> FindByStatus(int status);
        List<Wards> FindAllActive();
    }
} 