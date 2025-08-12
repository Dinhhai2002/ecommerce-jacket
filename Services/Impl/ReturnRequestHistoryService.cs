using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class ReturnRequestHistoryService : IReturnRequestHistoryService
    {
        private readonly IReturnRequestHistoryRepository _returnRequestHistoryRepository;

        public ReturnRequestHistoryService(IReturnRequestHistoryRepository returnRequestHistoryRepository)
        {
            _returnRequestHistoryRepository = returnRequestHistoryRepository;
        }

        public async Task<StoreProcedureListResult<ReturnRequestHistory>> GetList(int returnRequestId, string keySearch, int status, Pagination pagination)
        {
            return await _returnRequestHistoryRepository.SpGListReturnRequestHistory(returnRequestId, keySearch, status, pagination);
        }

        public async Task<ReturnRequestHistory> GetById(int id)
        {
            return await _returnRequestHistoryRepository.FindOne(id);
        }

        public async Task<ReturnRequestHistory> Create(ReturnRequestHistory returnRequestHistory)
        {
            await _returnRequestHistoryRepository.Create(returnRequestHistory);
            return returnRequestHistory;
        }

        public async Task<ReturnRequestHistory> Update(ReturnRequestHistory returnRequestHistory)
        {
            await _returnRequestHistoryRepository.Update(returnRequestHistory);
            return returnRequestHistory;
        }

        public async Task<List<ReturnRequestHistory>> GetAll()
        {
            return await _returnRequestHistoryRepository.GetAll().ToListAsync();
        }

        public async Task<List<ReturnRequestHistory>> GetByStatus(int status)
        {
            return await _returnRequestHistoryRepository.FindByCondition(h => h.Status == status).ToListAsync();
        }

        public async Task<List<ReturnRequestHistory>> GetByReturnRequestId(int returnRequestId)
        {
            return await _returnRequestHistoryRepository.FindByCondition(h => h.ReturnRequestId == returnRequestId).ToListAsync();
        }

        public async Task<List<ReturnRequestHistory>> FindAllActive()
        {
            return await _returnRequestHistoryRepository.FindByCondition(h => h.Status == 1).ToListAsync();
        }
    }
} 