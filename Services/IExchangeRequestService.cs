using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IExchangeRequestService
    {
        void Create(ExchangeRequest exchangeRequest);
        ExchangeRequest FindOne(int id);
        void Update(ExchangeRequest exchangeRequest);
        List<ExchangeRequest> GetAll();
        List<ExchangeRequest> FindByUserId(int userId);
        List<ExchangeRequest> FindByOrderId(int orderId);
        List<ExchangeRequest> FindByStatus(int status);
        List<ExchangeRequest> FindByUserIdAndStatus(int userId, int status);
        List<ExchangeRequest> FindAllActive();
    }
} 