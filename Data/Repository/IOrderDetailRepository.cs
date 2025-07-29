using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        List<OrderDetail> FindByOrderId(int orderId);
        List<OrderDetail> FindByProductDetailId(int productDetailId);
        List<OrderDetail> FindByStatus(int status);
        List<OrderDetail> FindAllActive();
        decimal GetTotalPriceByOrderId(int orderId);
    }
} 