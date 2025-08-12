using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IReviewService
    {
        Task<StoreProcedureListResult<Review>> GetList(int userId, int productId, string keySearch, int status, Pagination pagination);
        Task<Review> GetById(int id);
        Task<Review> Create(Review review);
        Task<Review> Update(Review review);
        Task<List<Review>> GetAll();
        Task<List<Review>> GetByStatus(int status);
        Task<List<Review>> GetByUserId(int userId);
        Task<List<Review>> GetByProductId(int productId);
        Task<List<Review>> GetByOrderId(int orderId);
        Task<double> GetAverageRating(int productId);
        Task<bool> HasUserReviewed(int userId, int productId);
    }
} 