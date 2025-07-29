using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;

        public CartDetailService(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }

        public void Create(CartDetail cartDetail)
        {
            _cartDetailRepository.Create(cartDetail);
        }

        public CartDetail FindOne(int id)
        {
            return _cartDetailRepository.FindOne(id);
        }

        public void Update(CartDetail cartDetail)
        {
            _cartDetailRepository.Update(cartDetail);
        }

        public void Delete(int id)
        {
            var cartDetail = _cartDetailRepository.FindOne(id);
            if (cartDetail != null)
            {
                _cartDetailRepository.Delete(id);
            }
        }

        public List<CartDetail> GetAll()
        {
            return _cartDetailRepository.GetAll();
        }

        public List<CartDetail> FindByCartId(int cartId)
        {
            return _cartDetailRepository.FindByCartId(cartId);
        }

        public List<CartDetail> FindByProductDetailId(int productDetailId)
        {
            return _cartDetailRepository.FindByProductDetailId(productDetailId);
        }

        public CartDetail FindByCartIdAndProductDetailId(int cartId, int productDetailId)
        {
            return _cartDetailRepository.FindByCartIdAndProductDetailId(cartId, productDetailId);
        }

        public List<CartDetail> FindByStatus(int status)
        {
            return _cartDetailRepository.FindByStatus(status);
        }

        public List<CartDetail> FindAllActive()
        {
            return _cartDetailRepository.FindAllActive();
        }
    }
} 