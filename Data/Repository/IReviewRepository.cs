using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        List<Review> FindByUserId(int userId);
        List<Review> FindByProductId(int productId);
        List<Review> FindByUserIdAndProductId(int userId, int productId);
        List<Review> FindByStatus(int status);
        double GetAverageRatingByProductId(int productId);
    }
} 