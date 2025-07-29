using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public void Create(Size size)
        {
            _sizeRepository.Create(size);
        }

        public Size FindOne(int id)
        {
            return _sizeRepository.FindOne(id);
        }

        public void Update(Size size)
        {
            _sizeRepository.Update(size);
        }

        public List<Size> GetAll()
        {
            return _sizeRepository.GetAll();
        }

        public Size FindByCode(string code)
        {
            return _sizeRepository.FindByCode(code);
        }

        public Size FindByName(string name)
        {
            return _sizeRepository.FindByName(name);
        }

        public List<Size> FindByStatus(int status)
        {
            return _sizeRepository.FindByStatus(status);
        }

        public List<Size> FindAllActive()
        {
            return _sizeRepository.FindAllActive();
        }
    }
} 