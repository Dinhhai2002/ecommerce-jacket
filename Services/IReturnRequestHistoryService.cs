using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IReturnRequestHistoryService
    {
        Task<StoreProcedureListResult<ReturnRequestHistory>> GetList(int returnRequestId, string keySearch, int status, Pagination pagination);
        Task<ReturnRequestHistory> GetById(int id);
        Task<ReturnRequestHistory> Create(ReturnRequestHistory returnRequestHistory);
        Task<ReturnRequestHistory> Update(ReturnRequestHistory returnRequestHistory);
        Task<List<ReturnRequestHistory>> GetAll();
        Task<List<ReturnRequestHistory>> GetByStatus(int status);
        Task<List<ReturnRequestHistory>> GetByReturnRequestId(int returnRequestId);
        Task<List<ReturnRequestHistory>> FindAllActive();
    }
} 