using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IReturnRequestRepository : IGenericRepository<ReturnRequest>
    {
        List<ReturnRequest> FindByUserId(int userId);
        List<ReturnRequest> FindByOrderId(int orderId);
        List<ReturnRequest> FindByStatus(int status);
        List<ReturnRequest> FindByUserIdAndStatus(int userId, int status);
        List<ReturnRequest> FindAllActive();
    }
} 