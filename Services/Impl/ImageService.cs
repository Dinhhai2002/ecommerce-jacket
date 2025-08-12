using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<StoreProcedureListResult<Image>> GetList(int? productId, int? bannerId, int? returnRequestId, int? reviewId, string keySearch, int status, Pagination pagination)
        {
            return await _imageRepository.SpGListImage(productId, bannerId, returnRequestId, reviewId, keySearch, status, pagination);
        }

        public async Task<Image> GetById(int id)
        {
            return await _imageRepository.FindOne(id);
        }

        public async Task<Image> Create(Image image)
        {
            await _imageRepository.Create(image);
            return image;
        }

        public async Task<Image> Update(Image image)
        {
            await _imageRepository.Update(image);
            return image;
        }

        public async Task<List<Image>> GetAll()
        {
            return await _imageRepository.GetAll().ToListAsync();
        }

        public async Task<List<Image>> GetByStatus(int status)
        {
            return await _imageRepository.FindByCondition(i => i.Status == status).ToListAsync();
        }

        public async Task<Image> GetByProductId(int productId)
        {
            return await _imageRepository.FindByProductId(productId);
        }

        public async Task<Image> GetByBannerId(int bannerId)
        {
            return await _imageRepository.FindByBannerId(bannerId);
        }

        public async Task<Image> GetByReturnRequestId(int returnRequestId)
        {
            return await _imageRepository.FindByReturnRequestId(returnRequestId);
        }

        public async Task<Image> GetByReviewId(int reviewId)
        {
            return await _imageRepository.FindByReviewId(reviewId);
        }
    }
} 