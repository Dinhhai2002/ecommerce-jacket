using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context)
        {
        }

        public List<OrderDetail> FindByOrderId(int orderId)
        {
            return _dbSet.Where(od => od.OrderId == orderId).ToList();
        }

        public List<OrderDetail> FindByProductDetailId(int productDetailId)
        {
            return _dbSet.Where(od => od.ProductDetailId == productDetailId).ToList();
        }

        public List<OrderDetail> FindByStatus(int status)
        {
            return _dbSet.Where(od => od.Status == status).ToList();
        }

        public List<OrderDetail> FindAllActive()
        {
            return _dbSet.Where(od => od.Status == 1).ToList();
        }

        public decimal GetTotalPriceByOrderId(int orderId)
        {
            return _dbSet.Where(od => od.OrderId == orderId)
                        .Sum(od => od.TotalPrice);
        }
    }
} 