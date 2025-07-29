using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface ICategoryService
    {
        void Create(Category category);
        Category FindOne(int id);
        void Update(Category category);
        List<Category> GetAll();
        Category FindByName(string name);
        List<Category> FindByParentId(int parentId);
        List<Category> FindByStatus(int status);
        List<Category> FindAllActive();
        List<Category> FindAllParent();
        List<Category> FindAllChild(int parentId);
    }
} 