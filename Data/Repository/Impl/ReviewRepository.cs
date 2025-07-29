using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public List<Review> FindByUserId(int userId)
        {
            return _dbSet.Where(r => r.UserId == userId).ToList();
        }

        public List<Review> FindByProductId(int productId)
        {
            return _dbSet.Where(r => r.ProductId == productId).ToList();
        }

        public List<Review> FindByUserIdAndProductId(int userId, int productId)
        {
            return _dbSet.Where(r => r.UserId == userId && r.ProductId == productId).ToList();
        }

        public List<Review> FindByStatus(int status)
        {
            return _dbSet.Where(r => r.Status == status).ToList();
        }

        public double GetAverageRatingByProductId(int productId)
        {
            var reviews = _dbSet.Where(r => r.ProductId == productId && r.Status == 1);
            if (!reviews.Any())
                return 0;
            return reviews.Average(r => r.Rating);
        }
    }
} 