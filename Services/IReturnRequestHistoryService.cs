using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IReturnRequestHistoryService
    {
        void Create(ReturnRequestHistory returnRequestHistory);
        ReturnRequestHistory FindOne(int id);
        void Update(ReturnRequestHistory returnRequestHistory);
        List<ReturnRequestHistory> GetAll();
        List<ReturnRequestHistory> FindByReturnRequestId(int returnRequestId);
        List<ReturnRequestHistory> FindByStatus(int status);
        List<ReturnRequestHistory> FindAllActive();
    }
} 