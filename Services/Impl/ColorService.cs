using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public void Create(Color color)
        {
            _colorRepository.Create(color);
        }

        public Color FindOne(int id)
        {
            return _colorRepository.FindOne(id);
        }

        public void Update(Color color)
        {
            _colorRepository.Update(color);
        }

        public List<Color> GetAll()
        {
            return _colorRepository.GetAll();
        }

        public Color FindByCode(string code)
        {
            return _colorRepository.FindByCode(code);
        }

        public Color FindByName(string name)
        {
            return _colorRepository.FindByName(name);
        }

        public List<Color> FindByStatus(int status)
        {
            return _colorRepository.FindByStatus(status);
        }

        public List<Color> FindAllActive()
        {
            return _colorRepository.FindAllActive();
        }
    }
} 