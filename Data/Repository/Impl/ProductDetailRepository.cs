using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ProductDetailRepository : GenericRepository<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(AppDbContext context) : base(context)
        {
        }

        public List<ProductDetail> FindByProductId(int productId)
        {
            return _dbSet.Where(pd => pd.ProductId == productId).ToList();
        }

        public List<ProductDetail> FindByColorId(int colorId)
        {
            return _dbSet.Where(pd => pd.ColorId == colorId).ToList();
        }

        public List<ProductDetail> FindBySizeId(int sizeId)
        {
            return _dbSet.Where(pd => pd.SizeId == sizeId).ToList();
        }

        public List<ProductDetail> FindByMaterialId(int materialId)
        {
            return _dbSet.Where(pd => pd.MaterialId == materialId).ToList();
        }

        public List<ProductDetail> FindByStatus(int status)
        {
            return _dbSet.Where(pd => pd.Status == status).ToList();
        }

        public List<ProductDetail> FindByIds(List<int> ids)
        {
            return _dbSet.Where(pd => ids.Contains(pd.Id)).ToList();
        }

        public List<ProductDetail> FindAllActive()
        {
            return _dbSet.Where(pd => pd.Status == 1).ToList();
        }
    }
} 