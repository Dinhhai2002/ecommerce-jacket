using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IBrandService
    {
        void Create(Brand brand);
        Brand FindOne(int id);
        void Update(Brand brand);
        List<Brand> GetAll();
        Brand FindByName(string name);
        List<Brand> FindByStatus(int status);
        List<Brand> FindAllActive();
    }
} 