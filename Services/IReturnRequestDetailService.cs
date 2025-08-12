using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IReturnRequestDetailService
    {
        Task<StoreProcedureListResult<ReturnRequestDetail>> GetList(int returnRequestId, string keySearch, int status, Pagination pagination);
        Task<ReturnRequestDetail> GetById(int id);
        Task<ReturnRequestDetail> Create(ReturnRequestDetail returnRequestDetail);
        Task<ReturnRequestDetail> Update(ReturnRequestDetail returnRequestDetail);
        Task<List<ReturnRequestDetail>> GetAll();
        Task<List<ReturnRequestDetail>> GetByStatus(int status);
        Task<List<ReturnRequestDetail>> GetByReturnRequestId(int returnRequestId);
        Task<List<ReturnRequestDetail>> GetByOrderDetailId(int orderDetailId);
    }
} 