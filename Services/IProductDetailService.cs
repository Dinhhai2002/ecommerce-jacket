using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IProductDetailService
    {
        void Create(ProductDetail productDetail);
        ProductDetail FindOne(int id);
        void Update(ProductDetail productDetail);
        List<ProductDetail> GetAll();
        List<ProductDetail> FindByProductId(int productId);
        List<ProductDetail> FindByColorId(int colorId);
        List<ProductDetail> FindBySizeId(int sizeId);
        List<ProductDetail> FindByMaterialId(int materialId);
        List<ProductDetail> FindByStatus(int status);
        List<ProductDetail> FindByIds(List<int> ids);
        List<ProductDetail> FindAllActive();
    }
} 