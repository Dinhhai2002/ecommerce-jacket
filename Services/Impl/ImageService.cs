using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void Create(Image image)
        {
            _imageRepository.Create(image);
        }

        public Image FindOne(int id)
        {
            return _imageRepository.FindOne(id);
        }

        public void Update(Image image)
        {
            _imageRepository.Update(image);
        }

        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public List<Image> FindByProductId(int productId)
        {
            return _imageRepository.FindByProductId(productId);
        }

        public List<Image> FindByStatus(int status)
        {
            return _imageRepository.FindByStatus(status);
        }

        public List<Image> FindAllActive()
        {
            return _imageRepository.FindAllActive();
        }
    }
} 