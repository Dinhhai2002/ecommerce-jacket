using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        List<Image> FindByProductId(int productId);
        List<Image> FindByStatus(int status);
        List<Image> FindAllActive();
    }
} 