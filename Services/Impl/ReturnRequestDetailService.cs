using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class ReturnRequestDetailService : IReturnRequestDetailService
    {
        private readonly IReturnRequestDetailRepository _returnRequestDetailRepository;

        public ReturnRequestDetailService(IReturnRequestDetailRepository returnRequestDetailRepository)
        {
            _returnRequestDetailRepository = returnRequestDetailRepository;
        }

        public async Task<StoreProcedureListResult<ReturnRequestDetail>> GetList(int returnRequestId, string keySearch, int status, Pagination pagination)
        {
            return await _returnRequestDetailRepository.SpGListReturnRequestDetail(returnRequestId, keySearch, status, pagination);
        }

        public async Task<ReturnRequestDetail> GetById(int id)
        {
            return await _returnRequestDetailRepository.FindOne(id);
        }

        public async Task<ReturnRequestDetail> Create(ReturnRequestDetail returnRequestDetail)
        {
            await _returnRequestDetailRepository.Create(returnRequestDetail);
            return returnRequestDetail;
        }

        public async Task<ReturnRequestDetail> Update(ReturnRequestDetail returnRequestDetail)
        {
            await _returnRequestDetailRepository.Update(returnRequestDetail);
            return returnRequestDetail;
        }

        public async Task<List<ReturnRequestDetail>> GetAll()
        {
            return await _returnRequestDetailRepository.GetAll().ToListAsync();
        }

        public async Task<List<ReturnRequestDetail>> GetByStatus(int status)
        {
            return await _returnRequestDetailRepository.FindByCondition(r => r.Status == status).ToListAsync();
        }

        public async Task<List<ReturnRequestDetail>> GetByReturnRequestId(int returnRequestId)
        {
            return await _returnRequestDetailRepository.FindByCondition(r => r.ReturnRequestId == returnRequestId).ToListAsync();
        }

        public async Task<List<ReturnRequestDetail>> GetByOrderDetailId(int orderDetailId)
        {
            return await _returnRequestDetailRepository.FindByCondition(r => r.OrderDetailId == orderDetailId).ToListAsync();
        }
    }
} 