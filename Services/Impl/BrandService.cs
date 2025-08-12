using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Data.Repository;
using webecommerce.Models;

namespace webecommerce.Services.Impl
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<StoreProcedureListResult<Brand>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _brandRepository.GetListAsync(searchKey, status, pagination);
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<Brand> CreateAsync(Brand brand)
        {
            return await _brandRepository.AddAsync(brand);
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            return await _brandRepository.UpdateAsync(brand);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _brandRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Brand>> GetByStatusAsync(int status)
        {
            return await _brandRepository.FindAsync(b => b.Status == status);
        }

        public async Task<Brand> GetByNameAsync(string name)
        {
            return await _brandRepository.GetByNameAsync(name);
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            return await _brandRepository.CheckNameExistsAsync(name);
        }

        public async Task<StoreProcedureListResult<Brand>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _brandRepository.GetListWithProductsAsync(searchKey, status, pagination);
        }

        public async Task<IEnumerable<Brand>> GetByIdsWithProductsAsync(IEnumerable<int> ids)
        {
            return await _brandRepository.GetByIdsWithProductsAsync(ids);
        }
    }
} 