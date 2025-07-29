using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Create(Category category)
        {
            _categoryRepository.Create(category);
        }

        public Category FindOne(int id)
        {
            return _categoryRepository.FindOne(id);
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category FindByName(string name)
        {
            return _categoryRepository.FindByName(name);
        }

        public List<Category> FindByParentId(int parentId)
        {
            return _categoryRepository.FindByParentId(parentId);
        }

        public List<Category> FindByStatus(int status)
        {
            return _categoryRepository.FindByStatus(status);
        }

        public List<Category> FindAllActive()
        {
            return _categoryRepository.FindAllActive();
        }

        public List<Category> FindAllParent()
        {
            return _categoryRepository.FindAllParent();
        }

        public List<Category> FindAllChild(int parentId)
        {
            return _categoryRepository.FindAllChild(parentId);
        }
    }
} 