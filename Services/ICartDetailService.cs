using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface ICartDetailService
    {
        void Create(CartDetail cartDetail);
        CartDetail FindOne(int id);
        void Update(CartDetail cartDetail);
        void Delete(int id);
        List<CartDetail> GetAll();
        List<CartDetail> FindByCartId(int cartId);
        List<CartDetail> FindByProductDetailId(int productDetailId);
        CartDetail FindByCartIdAndProductDetailId(int cartId, int productDetailId);
        List<CartDetail> FindByStatus(int status);
        List<CartDetail> FindAllActive();
    }
} 