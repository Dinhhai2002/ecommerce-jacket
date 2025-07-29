using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public void Create(OrderDetail orderDetail)
        {
            _orderDetailRepository.Create(orderDetail);
        }

        public OrderDetail FindOne(int id)
        {
            return _orderDetailRepository.FindOne(id);
        }

        public void Update(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
        }

        public List<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public List<OrderDetail> FindByOrderId(int orderId)
        {
            return _orderDetailRepository.FindByOrderId(orderId);
        }

        public List<OrderDetail> FindByProductDetailId(int productDetailId)
        {
            return _orderDetailRepository.FindByProductDetailId(productDetailId);
        }

        public List<OrderDetail> FindByStatus(int status)
        {
            return _orderDetailRepository.FindByStatus(status);
        }

        public List<OrderDetail> FindAllActive()
        {
            return _orderDetailRepository.FindAllActive();
        }

        public decimal GetTotalPriceByOrderId(int orderId)
        {
            return _orderDetailRepository.GetTotalPriceByOrderId(orderId);
        }
    }
} 