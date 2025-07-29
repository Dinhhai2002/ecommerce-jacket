using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {
        }

        public List<Image> FindByProductId(int productId)
        {
            return _dbSet.Where(i => i.ProductId == productId).ToList();
        }

        public List<Image> FindByStatus(int status)
        {
            return _dbSet.Where(i => i.Status == status).ToList();
        }

        public List<Image> FindAllActive()
        {
            return _dbSet.Where(i => i.Status == 1).ToList();
        }
    }
} 