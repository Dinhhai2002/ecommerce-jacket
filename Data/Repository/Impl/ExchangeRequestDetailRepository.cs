using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ExchangeRequestDetailRepository : GenericRepository<ExchangeRequestDetail>, IExchangeRequestDetailRepository
    {
        public ExchangeRequestDetailRepository(AppDbContext context) : base(context)
        {
        }

        public List<ExchangeRequestDetail> FindByExchangeRequestId(int exchangeRequestId)
        {
            return _dbSet.Where(erd => erd.ExchangeRequestId == exchangeRequestId).ToList();
        }

        public List<ExchangeRequestDetail> FindByOrderDetailId(int orderDetailId)
        {
            return _dbSet.Where(erd => erd.OrderDetailId == orderDetailId).ToList();
        }

        public List<ExchangeRequestDetail> FindByProductDetailId(int productDetailId)
        {
            return _dbSet.Where(erd => erd.ProductDetailId == productDetailId).ToList();
        }

        public List<ExchangeRequestDetail> FindByExchangeProductDetailId(int exchangeProductDetailId)
        {
            return _dbSet.Where(erd => erd.ExchangeProductDetailId == exchangeProductDetailId).ToList();
        }

        public List<ExchangeRequestDetail> FindByStatus(int status)
        {
            return _dbSet.Where(erd => erd.Status == status).ToList();
        }

        public List<ExchangeRequestDetail> FindAllActive()
        {
            return _dbSet.Where(erd => erd.Status == 1).ToList();
        }
    }
} 