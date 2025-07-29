using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IReturnRequestService
    {
        void Create(ReturnRequest returnRequest);
        ReturnRequest FindOne(int id);
        void Update(ReturnRequest returnRequest);
        List<ReturnRequest> GetAll();
        List<ReturnRequest> FindByUserId(int userId);
        List<ReturnRequest> FindByOrderId(int orderId);
        List<ReturnRequest> FindByStatus(int status);
        List<ReturnRequest> FindByUserIdAndStatus(int userId, int status);
        List<ReturnRequest> FindAllActive();
    }
} 