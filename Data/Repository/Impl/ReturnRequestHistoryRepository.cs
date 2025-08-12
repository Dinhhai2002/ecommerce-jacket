using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class ReturnRequestHistoryRepository : GenericRepository<ReturnRequestHistory>, IReturnRequestHistoryRepository
    {
        private readonly AppDbContext _context;

        public ReturnRequestHistoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StoreProcedureListResult<ReturnRequestHistory>> SpGListReturnRequestHistory(int returnRequestId, string keySearch, int status, Pagination pagination)
        {
            var query = _context.ReturnRequestHistories.AsQueryable();

            if (returnRequestId > 0)
                query = query.Where(h => h.ReturnRequestId == returnRequestId);

            if (!string.IsNullOrEmpty(keySearch))
                query = query.Where(h => h.Action.Contains(keySearch) || h.Note.Contains(keySearch));

            query = query.Where(h => h.Status == status);

            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new StoreProcedureListResult<ReturnRequestHistory>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public List<ReturnRequestHistory> FindByReturnRequestId(int returnRequestId)
        {
            return _context.ReturnRequestHistories
                .Where(h => h.ReturnRequestId == returnRequestId)
                .ToList();
        }

        public List<ReturnRequestHistory> FindByStatus(int status)
        {
            return _context.ReturnRequestHistories
                .Where(h => h.Status == status)
                .ToList();
        }

        public List<ReturnRequestHistory> FindAllActive()
        {
            return _context.ReturnRequestHistories
                .Where(h => h.Status == 1)
                .ToList();
        }
    }
} 