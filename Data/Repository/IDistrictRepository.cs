using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IDistrictRepository : IGenericRepository<Districts>
    {
        Districts FindByCode(string code);
        List<Districts> FindByCityId(int cityId);
        List<Districts> FindByStatus(int status);
        List<Districts> FindAllActive();
    }
} 