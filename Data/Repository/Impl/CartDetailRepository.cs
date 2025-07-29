using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class CartDetailRepository : GenericRepository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(AppDbContext context) : base(context)
        {
        }

        public List<CartDetail> FindByCartId(int cartId)
        {
            return _dbSet.Where(cd => cd.CartId == cartId).ToList();
        }

        public List<CartDetail> FindByProductDetailId(int productDetailId)
        {
            return _dbSet.Where(cd => cd.ProductDetailId == productDetailId).ToList();
        }

        public CartDetail FindByCartIdAndProductDetailId(int cartId, int productDetailId)
        {
            return _dbSet.FirstOrDefault(cd => cd.CartId == cartId && cd.ProductDetailId == productDetailId);
        }

        public List<CartDetail> FindByStatus(int status)
        {
            return _dbSet.Where(cd => cd.Status == status).ToList();
        }

        public List<CartDetail> FindAllActive()
        {
            return _dbSet.Where(cd => cd.Status == 1).ToList();
        }
    }
} 