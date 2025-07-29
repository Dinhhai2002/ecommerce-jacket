using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface ISizeService
    {
        void Create(Size size);
        Size FindOne(int id);
        void Update(Size size);
        List<Size> GetAll();
        Size FindByCode(string code);
        Size FindByName(string name);
        List<Size> FindByStatus(int status);
        List<Size> FindAllActive();
    }
} 