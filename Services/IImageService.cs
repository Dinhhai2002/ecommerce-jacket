using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IImageService
    {
        void Create(Image image);
        Image FindOne(int id);
        void Update(Image image);
        List<Image> GetAll();
        List<Image> FindByProductId(int productId);
        List<Image> FindByStatus(int status);
        List<Image> FindAllActive();
    }
} 