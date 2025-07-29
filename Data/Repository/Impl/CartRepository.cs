using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public List<Cart> FindByUserId(int userId)
        {
            return _dbSet.Where(c => c.UserId == userId).ToList();
        }

        public Cart FindByUserIdAndStatus(int userId, int status)
        {
            return _dbSet.FirstOrDefault(c => c.UserId == userId && c.Status == status);
        }

        public List<Cart> FindByStatus(int status)
        {
            return _dbSet.Where(c => c.Status == status).ToList();
        }
    }
} 