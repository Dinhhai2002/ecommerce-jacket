using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IImageService
    {
        Task<StoreProcedureListResult<Image>> GetList(int? productId, int? bannerId, int? returnRequestId, int? reviewId, string keySearch, int status, Pagination pagination);
        Task<Image> GetById(int id);
        Task<Image> Create(Image image);
        Task<Image> Update(Image image);
        Task<List<Image>> GetAll();
        Task<List<Image>> GetByStatus(int status);
        Task<Image> GetByProductId(int productId);
        Task<Image> GetByBannerId(int bannerId);
        Task<Image> GetByReturnRequestId(int returnRequestId);
        Task<Image> GetByReviewId(int reviewId);
    }
} 