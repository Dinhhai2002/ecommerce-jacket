using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ExchangeRequestService : IExchangeRequestService
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepository;

        public ExchangeRequestService(IExchangeRequestRepository exchangeRequestRepository)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
        }

        public void Create(ExchangeRequest exchangeRequest)
        {
            _exchangeRequestRepository.Create(exchangeRequest);
        }

        public ExchangeRequest FindOne(int id)
        {
            return _exchangeRequestRepository.FindOne(id);
        }

        public void Update(ExchangeRequest exchangeRequest)
        {
            _exchangeRequestRepository.Update(exchangeRequest);
        }

        public List<ExchangeRequest> GetAll()
        {
            return _exchangeRequestRepository.GetAll();
        }

        public List<ExchangeRequest> FindByUserId(int userId)
        {
            return _exchangeRequestRepository.FindByUserId(userId);
        }

        public List<ExchangeRequest> FindByOrderId(int orderId)
        {
            return _exchangeRequestRepository.FindByOrderId(orderId);
        }

        public List<ExchangeRequest> FindByStatus(int status)
        {
            return _exchangeRequestRepository.FindByStatus(status);
        }

        public List<ExchangeRequest> FindByUserIdAndStatus(int userId, int status)
        {
            return _exchangeRequestRepository.FindByUserIdAndStatus(userId, status);
        }

        public List<ExchangeRequest> FindAllActive()
        {
            return _exchangeRequestRepository.FindAllActive();
        }
    }
} 