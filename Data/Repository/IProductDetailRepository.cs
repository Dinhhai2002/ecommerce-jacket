using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IProductDetailRepository : IGenericRepository<ProductDetail>
    {
        List<ProductDetail> FindByProductId(int productId);
        List<ProductDetail> FindByColorId(int colorId);
        List<ProductDetail> FindBySizeId(int sizeId);
        List<ProductDetail> FindByMaterialId(int materialId);
        List<ProductDetail> FindByStatus(int status);
        List<ProductDetail> FindByIds(List<int> ids);
        List<ProductDetail> FindAllActive();
    }
} 