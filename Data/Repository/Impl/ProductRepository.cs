using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public List<Product> FindByIds(List<int> ids)
        {
            return _dbSet.Where(p => ids.Contains(p.Id)).ToList();
        }

        public Product FindByName(string name)
        {
            return _dbSet.FirstOrDefault(p => p.Name == name);
        }

        public List<Product> FindAllActive()
        {
            return _dbSet.Where(p => p.Status == 1).ToList();
        }

        public List<Product> FindByBrandId(int brandId)
        {
            return _dbSet.Where(p => p.BrandId == brandId).ToList();
        }

        public List<Product> FindByCategoryId(int categoryId)
        {
            return _dbSet.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
} 