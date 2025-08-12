using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IReturnRequestService
    {
        Task<StoreProcedureListResult<ReturnRequest>> GetList(int userId, string keySearch, string status, Pagination pagination);
        Task<ReturnRequest> GetById(int id);
        Task<ReturnRequest> Create(ReturnRequestRequest request);
        Task<ReturnRequest> Update(ReturnRequest returnRequest);
        Task<List<ReturnRequest>> GetAll();
        Task<List<ReturnRequest>> GetByStatus(string status);
        Task<List<ReturnRequest>> GetByUserId(int userId);
        Task<List<ReturnRequest>> GetByOrderId(int orderId);
        Task<ReturnRequest> Approve(int id, ApproveReturnRequestRequest request);
        Task<ReturnRequest> Reject(int id, RejectReturnRequestRequest request);
        Task<ReturnRequest> Process(int id);
        Task<ReturnRequest> Complete(int id);
        Task Cancel(int id);
        Task Delete(int id);
    }
} 