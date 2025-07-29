using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        List<Cart> FindByUserId(int userId);
        Cart FindByUserIdAndStatus(int userId, int status);
        List<Cart> FindByStatus(int status);
    }
} 