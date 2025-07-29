using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IExchangeRequestRepository : IGenericRepository<ExchangeRequest>
    {
        List<ExchangeRequest> FindByUserId(int userId);
        List<ExchangeRequest> FindByOrderId(int orderId);
        List<ExchangeRequest> FindByStatus(int status);
        List<ExchangeRequest> FindByUserIdAndStatus(int userId, int status);
        List<ExchangeRequest> FindAllActive();
    }
} 