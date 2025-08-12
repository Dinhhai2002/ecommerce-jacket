using System.Collections.Generic;
using webecommerce.Models;

namespace webecommerce.Data.Repository
{
    public interface IReturnRequestHistoryRepository : IGenericRepository<ReturnRequestHistory>
    {
        List<ReturnRequestHistory> FindByReturnRequestId(int returnRequestId);
        List<ReturnRequestHistory> FindByStatus(int status);
        List<ReturnRequestHistory> FindAllActive();
    }
} 