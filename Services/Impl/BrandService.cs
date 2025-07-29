using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public void Create(Brand brand)
        {
            _brandRepository.Create(brand);
        }

        public Brand FindOne(int id)
        {
            return _brandRepository.FindOne(id);
        }

        public void Update(Brand brand)
        {
            _brandRepository.Update(brand);
        }

        public List<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public Brand FindByName(string name)
        {
            return _brandRepository.FindByName(name);
        }

        public List<Brand> FindByStatus(int status)
        {
            return _brandRepository.FindByStatus(status);
        }

        public List<Brand> FindAllActive()
        {
            return _brandRepository.FindAllActive();
        }
    }
} 