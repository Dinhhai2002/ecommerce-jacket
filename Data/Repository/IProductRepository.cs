using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> FindByIds(List<int> ids);
        Product FindByName(string name);
        List<Product> FindAllActive();
        List<Product> FindByBrandId(int brandId);
        List<Product> FindByCategoryId(int categoryId);
    }
} 