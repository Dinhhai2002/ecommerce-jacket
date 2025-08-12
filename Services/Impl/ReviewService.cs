using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<StoreProcedureListResult<Review>> GetList(int userId, int productId, string keySearch, int status, Pagination pagination)
        {
            return await _reviewRepository.SpGListReview(userId, productId, keySearch, status, pagination);
        }

        public async Task<Review> GetById(int id)
        {
            return await _reviewRepository.FindOne(id);
        }

        public async Task<Review> Create(Review review)
        {
            await _reviewRepository.Create(review);
            return review;
        }

        public async Task<Review> Update(Review review)
        {
            await _reviewRepository.Update(review);
            return review;
        }

        public async Task<List<Review>> GetAll()
        {
            return await _reviewRepository.GetAll().ToListAsync();
        }

        public async Task<List<Review>> GetByStatus(int status)
        {
            return await _reviewRepository.FindByCondition(r => r.Status == status).ToListAsync();
        }

        public async Task<List<Review>> GetByUserId(int userId)
        {
            return await _reviewRepository.FindByCondition(r => r.UserId == userId).ToListAsync();
        }

        public async Task<List<Review>> GetByProductId(int productId)
        {
            return await _reviewRepository.FindByCondition(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<List<Review>> GetByOrderId(int orderId)
        {
            return await _reviewRepository.FindByCondition(r => r.OrderId == orderId).ToListAsync();
        }

        public async Task<double> GetAverageRating(int productId)
        {
            var reviews = await _reviewRepository.FindByCondition(r => r.ProductId == productId && r.Status == 1).ToListAsync();
            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }

        public async Task<bool> HasUserReviewed(int userId, int productId)
        {
            var review = await _reviewRepository.FindByCondition(r => r.UserId == userId && r.ProductId == productId).FirstOrDefaultAsync();
            return review != null;
        }
    }
} 