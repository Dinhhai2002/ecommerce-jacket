using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task<StoreProcedureListResult<Image>> SpGListImage(int? productId, int? bannerId, int? returnRequestId, int? reviewId, string keySearch, int status, Pagination pagination);
        Task<Image> FindByProductId(int productId);
        Task<Image> FindByBannerId(int bannerId);
        Task<Image> FindByReturnRequestId(int returnRequestId);
        Task<Image> FindByReviewId(int reviewId);
    }
} 