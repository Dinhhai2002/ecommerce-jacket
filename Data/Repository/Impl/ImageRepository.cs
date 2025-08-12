using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly AppDbContext _context;

        public ImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StoreProcedureListResult<Image>> SpGListImage(int? productId, int? bannerId, int? returnRequestId, int? reviewId, string keySearch, int status, Pagination pagination)
        {
            var query = _context.Images.AsQueryable();

            if (productId.HasValue)
                query = query.Where(i => i.ProductId == productId);

            if (bannerId.HasValue)
                query = query.Where(i => i.BannerId == bannerId);

            if (returnRequestId.HasValue)
                query = query.Where(i => i.ReturnRequestId == returnRequestId);

            if (reviewId.HasValue)
                query = query.Where(i => i.ReviewId == reviewId);

            if (!string.IsNullOrEmpty(keySearch))
                query = query.Where(i => i.Title.Contains(keySearch) || i.Alt.Contains(keySearch));

            query = query.Where(i => i.Status == status);

            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new StoreProcedureListResult<Image>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<Image> FindByProductId(int productId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.ProductId == productId);
        }

        public async Task<Image> FindByBannerId(int bannerId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.BannerId == bannerId);
        }

        public async Task<Image> FindByReturnRequestId(int returnRequestId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.ReturnRequestId == returnRequestId);
        }

        public async Task<Image> FindByReviewId(int reviewId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.ReviewId == reviewId);
        }
    }
} 