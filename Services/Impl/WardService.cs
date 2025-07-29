using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class WardService : IWardService
    {
        private readonly IWardRepository _wardRepository;

        public WardService(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }

        public void Create(Wards ward)
        {
            _wardRepository.Create(ward);
        }

        public Wards FindOne(int id)
        {
            return _wardRepository.FindOne(id);
        }

        public void Update(Wards ward)
        {
            _wardRepository.Update(ward);
        }

        public List<Wards> GetAll()
        {
            return _wardRepository.GetAll();
        }

        public Wards FindByCode(string code)
        {
            return _wardRepository.FindByCode(code);
        }

        public List<Wards> FindByDistrictId(int districtId)
        {
            return _wardRepository.FindByDistrictId(districtId);
        }

        public List<Wards> FindByStatus(int status)
        {
            return _wardRepository.FindByStatus(status);
        }

        public List<Wards> FindAllActive()
        {
            return _wardRepository.FindAllActive();
        }
    }
} 