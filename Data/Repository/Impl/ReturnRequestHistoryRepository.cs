using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ReturnRequestHistoryRepository : GenericRepository<ReturnRequestHistory>, IReturnRequestHistoryRepository
    {
        public ReturnRequestHistoryRepository(AppDbContext context) : base(context)
        {
        }

        public List<ReturnRequestHistory> FindByReturnRequestId(int returnRequestId)
        {
            return _dbSet.Where(rrh => rrh.ReturnRequestId == returnRequestId).ToList();
        }

        public List<ReturnRequestHistory> FindByStatus(int status)
        {
            return _dbSet.Where(rrh => rrh.Status == status).ToList();
        }

        public List<ReturnRequestHistory> FindAllActive()
        {
            return _dbSet.Where(rrh => rrh.Status == 1).ToList();
        }
    }
} 