using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IOrderDetailService
    {
        void Create(OrderDetail orderDetail);
        OrderDetail FindOne(int id);
        void Update(OrderDetail orderDetail);
        List<OrderDetail> GetAll();
        List<OrderDetail> FindByOrderId(int orderId);
        List<OrderDetail> FindByProductDetailId(int productDetailId);
        List<OrderDetail> FindByStatus(int status);
        List<OrderDetail> FindAllActive();
        decimal GetTotalPriceByOrderId(int orderId);
    }
} 