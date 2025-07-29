using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictService(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public void Create(Districts district)
        {
            _districtRepository.Create(district);
        }

        public Districts FindOne(int id)
        {
            return _districtRepository.FindOne(id);
        }

        public void Update(Districts district)
        {
            _districtRepository.Update(district);
        }

        public List<Districts> GetAll()
        {
            return _districtRepository.GetAll();
        }

        public Districts FindByCode(string code)
        {
            return _districtRepository.FindByCode(code);
        }

        public List<Districts> FindByCityId(int cityId)
        {
            return _districtRepository.FindByCityId(cityId);
        }

        public List<Districts> FindByStatus(int status)
        {
            return _districtRepository.FindByStatus(status);
        }

        public List<Districts> FindAllActive()
        {
            return _districtRepository.FindAllActive();
        }
    }
} 