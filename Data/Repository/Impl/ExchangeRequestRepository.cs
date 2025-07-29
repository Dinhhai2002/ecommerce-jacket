using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ExchangeRequestRepository : GenericRepository<ExchangeRequest>, IExchangeRequestRepository
    {
        public ExchangeRequestRepository(AppDbContext context) : base(context)
        {
        }

        public List<ExchangeRequest> FindByUserId(int userId)
        {
            return _dbSet.Where(er => er.UserId == userId).ToList();
        }

        public List<ExchangeRequest> FindByOrderId(int orderId)
        {
            return _dbSet.Where(er => er.OrderId == orderId).ToList();
        }

        public List<ExchangeRequest> FindByStatus(int status)
        {
            return _dbSet.Where(er => er.Status == status).ToList();
        }

        public List<ExchangeRequest> FindByUserIdAndStatus(int userId, int status)
        {
            return _dbSet.Where(er => er.UserId == userId && er.Status == status).ToList();
        }

        public List<ExchangeRequest> FindAllActive()
        {
            return _dbSet.Where(er => er.Status == 1).ToList();
        }
    }
} 