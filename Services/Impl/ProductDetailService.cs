using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IProductDetailRepository _productDetailRepository;

        public ProductDetailService(IProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }

        public void Create(ProductDetail productDetail)
        {
            _productDetailRepository.Create(productDetail);
        }

        public ProductDetail FindOne(int id)
        {
            return _productDetailRepository.FindOne(id);
        }

        public void Update(ProductDetail productDetail)
        {
            _productDetailRepository.Update(productDetail);
        }

        public List<ProductDetail> GetAll()
        {
            return _productDetailRepository.GetAll();
        }

        public List<ProductDetail> FindByProductId(int productId)
        {
            return _productDetailRepository.FindByProductId(productId);
        }

        public List<ProductDetail> FindByColorId(int colorId)
        {
            return _productDetailRepository.FindByColorId(colorId);
        }

        public List<ProductDetail> FindBySizeId(int sizeId)
        {
            return _productDetailRepository.FindBySizeId(sizeId);
        }

        public List<ProductDetail> FindByMaterialId(int materialId)
        {
            return _productDetailRepository.FindByMaterialId(materialId);
        }

        public List<ProductDetail> FindByStatus(int status)
        {
            return _productDetailRepository.FindByStatus(status);
        }

        public List<ProductDetail> FindByIds(List<int> ids)
        {
            return _productDetailRepository.FindByIds(ids);
        }

        public List<ProductDetail> FindAllActive()
        {
            return _productDetailRepository.FindAllActive();
        }
    }
} 