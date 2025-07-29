using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public void Create(Cities city)
        {
            _cityRepository.Create(city);
        }

        public Cities FindOne(int id)
        {
            return _cityRepository.FindOne(id);
        }

        public void Update(Cities city)
        {
            _cityRepository.Update(city);
        }

        public List<Cities> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public Cities FindByCode(string code)
        {
            return _cityRepository.FindByCode(code);
        }

        public List<Cities> FindByStatus(int status)
        {
            return _cityRepository.FindByStatus(status);
        }

        public List<Cities> FindAllActive()
        {
            return _cityRepository.FindAllActive();
        }
    }
} 