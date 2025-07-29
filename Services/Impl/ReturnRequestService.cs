using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ReturnRequestService : IReturnRequestService
    {
        private readonly IReturnRequestRepository _returnRequestRepository;

        public ReturnRequestService(IReturnRequestRepository returnRequestRepository)
        {
            _returnRequestRepository = returnRequestRepository;
        }

        public void Create(ReturnRequest returnRequest)
        {
            _returnRequestRepository.Create(returnRequest);
        }

        public ReturnRequest FindOne(int id)
        {
            return _returnRequestRepository.FindOne(id);
        }

        public void Update(ReturnRequest returnRequest)
        {
            _returnRequestRepository.Update(returnRequest);
        }

        public List<ReturnRequest> GetAll()
        {
            return _returnRequestRepository.GetAll();
        }

        public List<ReturnRequest> FindByUserId(int userId)
        {
            return _returnRequestRepository.FindByUserId(userId);
        }

        public List<ReturnRequest> FindByOrderId(int orderId)
        {
            return _returnRequestRepository.FindByOrderId(orderId);
        }

        public List<ReturnRequest> FindByStatus(int status)
        {
            return _returnRequestRepository.FindByStatus(status);
        }

        public List<ReturnRequest> FindByUserIdAndStatus(int userId, int status)
        {
            return _returnRequestRepository.FindByUserIdAndStatus(userId, status);
        }

        public List<ReturnRequest> FindAllActive()
        {
            return _returnRequestRepository.FindAllActive();
        }
    }
} 