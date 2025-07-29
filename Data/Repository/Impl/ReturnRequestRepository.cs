using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ReturnRequestRepository : GenericRepository<ReturnRequest>, IReturnRequestRepository
    {
        public ReturnRequestRepository(AppDbContext context) : base(context)
        {
        }

        public List<ReturnRequest> FindByUserId(int userId)
        {
            return _dbSet.Where(rr => rr.UserId == userId).ToList();
        }

        public List<ReturnRequest> FindByOrderId(int orderId)
        {
            return _dbSet.Where(rr => rr.OrderId == orderId).ToList();
        }

        public List<ReturnRequest> FindByStatus(int status)
        {
            return _dbSet.Where(rr => rr.Status == status).ToList();
        }

        public List<ReturnRequest> FindByUserIdAndStatus(int userId, int status)
        {
            return _dbSet.Where(rr => rr.UserId == userId && rr.Status == status).ToList();
        }

        public List<ReturnRequest> FindAllActive()
        {
            return _dbSet.Where(rr => rr.Status == 1).ToList();
        }
    }
} 