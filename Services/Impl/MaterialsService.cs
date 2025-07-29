using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IMaterialsRepository _materialsRepository;

        public MaterialsService(IMaterialsRepository materialsRepository)
        {
            _materialsRepository = materialsRepository;
        }

        public void Create(Materials materials)
        {
            _materialsRepository.Create(materials);
        }

        public Materials FindOne(int id)
        {
            return _materialsRepository.FindOne(id);
        }

        public void Update(Materials materials)
        {
            _materialsRepository.Update(materials);
        }

        public List<Materials> GetAll()
        {
            return _materialsRepository.GetAll();
        }

        public Materials FindByCode(string code)
        {
            return _materialsRepository.FindByCode(code);
        }

        public Materials FindByName(string name)
        {
            return _materialsRepository.FindByName(name);
        }

        public List<Materials> FindByStatus(int status)
        {
            return _materialsRepository.FindByStatus(status);
        }

        public List<Materials> FindAllActive()
        {
            return _materialsRepository.FindAllActive();
        }
    }
} 