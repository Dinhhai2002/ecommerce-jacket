using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ReturnRequestHistoryService : IReturnRequestHistoryService
    {
        private readonly IReturnRequestHistoryRepository _returnRequestHistoryRepository;

        public ReturnRequestHistoryService(IReturnRequestHistoryRepository returnRequestHistoryRepository)
        {
            _returnRequestHistoryRepository = returnRequestHistoryRepository;
        }

        public void Create(ReturnRequestHistory returnRequestHistory)
        {
            _returnRequestHistoryRepository.Create(returnRequestHistory);
        }

        public ReturnRequestHistory FindOne(int id)
        {
            return _returnRequestHistoryRepository.FindOne(id);
        }

        public void Update(ReturnRequestHistory returnRequestHistory)
        {
            _returnRequestHistoryRepository.Update(returnRequestHistory);
        }

        public List<ReturnRequestHistory> GetAll()
        {
            return _returnRequestHistoryRepository.GetAll();
        }

        public List<ReturnRequestHistory> FindByReturnRequestId(int returnRequestId)
        {
            return _returnRequestHistoryRepository.FindByReturnRequestId(returnRequestId);
        }

        public List<ReturnRequestHistory> FindByStatus(int status)
        {
            return _returnRequestHistoryRepository.FindByStatus(status);
        }

        public List<ReturnRequestHistory> FindAllActive()
        {
            return _returnRequestHistoryRepository.FindAllActive();
        }
    }
} 