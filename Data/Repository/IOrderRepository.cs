using System.Collections.Generic;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> FindByUserId(int userId);
        List<Order> FindByStatus(int status);
        List<Order> FindByUserIdAndStatus(int userId, int status);
        Order FindByOrderCode(string orderCode);
        StoreProcedureListResult<Order> SpGListOrder(int userId, string keySearch, int status, int paymentStatus, int paymentMethod, Pagination pagination);
    }
} 