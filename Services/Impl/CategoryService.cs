using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Common.Utils;
using webecommerce.Data.Repository;
using webecommerce.Models;

namespace webecommerce.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<StoreProcedureListResult<Category>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _categoryRepository.GetListAsync(searchKey, status, pagination);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetByStatusAsync(int status)
        {
            return await _categoryRepository.FindAsync(c => c.Status == status);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _categoryRepository.GetByNameAsync(name);
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            return await _categoryRepository.CheckNameExistsAsync(name);
        }

        public async Task<StoreProcedureListResult<Category>> GetListWithProductsAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            return await _categoryRepository.GetListWithProductsAsync(searchKey, status, pagination);
        }

        public async Task<IEnumerable<Category>> GetByIdsWithProductsAsync(IEnumerable<int> ids)
        {
            return await _categoryRepository.GetByIdsWithProductsAsync(ids);
        }

        public async Task<IEnumerable<Category>> GetRootCategoriesAsync()
        {
            return await _categoryRepository.GetRootCategoriesAsync();
        }

        public async Task<IEnumerable<Category>> GetChildrenAsync(int parentId)
        {
            return await _categoryRepository.GetChildrenAsync(parentId);
        }
    }
} 