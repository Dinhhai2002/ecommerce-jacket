using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface ICartDetailRepository : IGenericRepository<CartDetail>
    {
        List<CartDetail> FindByCartId(int cartId);
        List<CartDetail> FindByProductDetailId(int productDetailId);
        CartDetail FindByCartIdAndProductDetailId(int cartId, int productDetailId);
        List<CartDetail> FindByStatus(int status);
        List<CartDetail> FindAllActive();
    }
} 