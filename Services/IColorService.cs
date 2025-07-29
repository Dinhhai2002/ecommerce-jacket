using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IColorService
    {
        void Create(Color color);
        Color FindOne(int id);
        void Update(Color color);
        List<Color> GetAll();
        Color FindByCode(string code);
        Color FindByName(string name);
        List<Color> FindByStatus(int status);
        List<Color> FindAllActive();
    }
} 