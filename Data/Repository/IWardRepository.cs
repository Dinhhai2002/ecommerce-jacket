using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IWardRepository : IGenericRepository<Wards>
    {
        Wards FindByCode(string code);
        List<Wards> FindByDistrictId(int districtId);
        List<Wards> FindByStatus(int status);
        List<Wards> FindAllActive();
    }
} 