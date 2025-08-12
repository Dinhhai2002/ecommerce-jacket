using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Data.Repository;
using webecommerce.Models;

namespace webecommerce.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<StoreProcedureListResult<Product>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _productRepository.GetListAsync(searchKey, status, pagination);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetByStatusAsync(int status)
        {
            return await _productRepository.FindAsync(p => p.Status == status);
        }

        public async Task<Product> GetBySkuAsync(string sku)
        {
            return await _productRepository.GetBySkuAsync(sku);
        }

        public async Task<bool> CheckSkuExistsAsync(string sku)
        {
            return await _productRepository.CheckSkuExistsAsync(sku);
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            return await _productRepository.CheckNameExistsAsync(name);
        }

        public async Task<IEnumerable<Product>> GetByBrandIdAsync(int brandId)
        {
            return await _productRepository.GetByBrandIdAsync(brandId);
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<StoreProcedureListResult<Product>> GetListByBrandAsync(int brandId, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _productRepository.GetListByBrandAsync(brandId, searchKey, status, pagination);
        }

        public async Task<StoreProcedureListResult<Product>> GetListByCategoryAsync(int categoryId, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _productRepository.GetListByCategoryAsync(categoryId, searchKey, status, pagination);
        }

        public async Task<StoreProcedureListResult<Product>> GetListByPriceRangeAsync(decimal minPrice, decimal maxPrice, string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _productRepository.GetListByPriceRangeAsync(minPrice, maxPrice, searchKey, status, pagination);
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _productRepository.GetByIdsAsync(ids);
        }
    }
} 