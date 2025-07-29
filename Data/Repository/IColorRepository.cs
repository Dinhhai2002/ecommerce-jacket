using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        Color FindByCode(string code);
        Color FindByName(string name);
        List<Color> FindByStatus(int status);
        List<Color> FindAllActive();
    }
} 