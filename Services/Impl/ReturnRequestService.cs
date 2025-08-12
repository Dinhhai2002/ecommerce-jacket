using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class ReturnRequestService : IReturnRequestService
    {
        private readonly IReturnRequestRepository _returnRequestRepository;
        private readonly IReturnRequestDetailRepository _returnRequestDetailRepository;
        private readonly IReturnRequestHistoryRepository _returnRequestHistoryRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public ReturnRequestService(
            IReturnRequestRepository returnRequestRepository,
            IReturnRequestDetailRepository returnRequestDetailRepository,
            IReturnRequestHistoryRepository returnRequestHistoryRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _returnRequestRepository = returnRequestRepository;
            _returnRequestDetailRepository = returnRequestDetailRepository;
            _returnRequestHistoryRepository = returnRequestHistoryRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<StoreProcedureListResult<ReturnRequest>> GetList(int userId, string keySearch, string status, Pagination pagination)
        {
            return await _returnRequestRepository.SpGListReturnRequest(userId, keySearch, status, pagination);
        }

        public async Task<ReturnRequest> GetById(int id)
        {
            return await _returnRequestRepository.FindOne(id);
        }

        public async Task<ReturnRequest> Create(ReturnRequestRequest request)
        {
            var returnRequest = new ReturnRequest
            {
                OrderId = request.OrderId,
                Reason = request.Reason,
                Note = request.Note,
                Status = 1 // Pending
            };

            await _returnRequestRepository.Create(returnRequest);

            foreach (var detail in request.Details)
            {
                var returnRequestDetail = new ReturnRequestDetail
                {
                    ReturnRequestId = returnRequest.Id,
                    OrderDetailId = detail.OrderDetailId,
                    Quantity = detail.Quantity,
                    Note = detail.Note,
                    Status = 1
                };

                await _returnRequestDetailRepository.Create(returnRequestDetail);
            }

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Created",
                Note = "Return request created",
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);

            return returnRequest;
        }

        public async Task<ReturnRequest> Update(ReturnRequest returnRequest)
        {
            await _returnRequestRepository.Update(returnRequest);
            return returnRequest;
        }

        public async Task<List<ReturnRequest>> GetAll()
        {
            return await _returnRequestRepository.GetAll().ToListAsync();
        }

        public async Task<List<ReturnRequest>> GetByStatus(string status)
        {
            return await _returnRequestRepository.FindByCondition(r => r.Status.ToString() == status).ToListAsync();
        }

        public async Task<List<ReturnRequest>> GetByUserId(int userId)
        {
            return await _returnRequestRepository.FindByCondition(r => r.UserId == userId).ToListAsync();
        }

        public async Task<List<ReturnRequest>> GetByOrderId(int orderId)
        {
            return await _returnRequestRepository.FindByCondition(r => r.OrderId == orderId).ToListAsync();
        }

        public async Task<ReturnRequest> Approve(int id, ApproveReturnRequestRequest request)
        {
            var returnRequest = await _returnRequestRepository.FindOne(id);
            if (returnRequest == null)
                throw new Exception("Return request not found");

            returnRequest.RefundAmount = request.RefundAmount;
            returnRequest.Status = 2; // Approved
            await _returnRequestRepository.Update(returnRequest);

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Approved",
                Note = request.Note,
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);

            return returnRequest;
        }

        public async Task<ReturnRequest> Reject(int id, RejectReturnRequestRequest request)
        {
            var returnRequest = await _returnRequestRepository.FindOne(id);
            if (returnRequest == null)
                throw new Exception("Return request not found");

            returnRequest.Status = 3; // Rejected
            await _returnRequestRepository.Update(returnRequest);

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Rejected",
                Note = request.Note,
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);

            return returnRequest;
        }

        public async Task<ReturnRequest> Process(int id)
        {
            var returnRequest = await _returnRequestRepository.FindOne(id);
            if (returnRequest == null)
                throw new Exception("Return request not found");

            returnRequest.Status = 4; // Processing
            await _returnRequestRepository.Update(returnRequest);

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Processing",
                Note = "Return request is being processed",
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);

            return returnRequest;
        }

        public async Task<ReturnRequest> Complete(int id)
        {
            var returnRequest = await _returnRequestRepository.FindOne(id);
            if (returnRequest == null)
                throw new Exception("Return request not found");

            returnRequest.Status = 5; // Completed
            await _returnRequestRepository.Update(returnRequest);

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Completed",
                Note = "Return request completed",
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);

            return returnRequest;
        }

        public async Task Cancel(int id)
        {
            var returnRequest = await _returnRequestRepository.FindOne(id);
            if (returnRequest == null)
                throw new Exception("Return request not found");

            returnRequest.Status = 6; // Cancelled
            await _returnRequestRepository.Update(returnRequest);

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Cancelled",
                Note = "Return request cancelled",
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);
        }

        public async Task Delete(int id)
        {
            var returnRequest = await _returnRequestRepository.FindOne(id);
            if (returnRequest == null)
                throw new Exception("Return request not found");

            returnRequest.Status = 0; // Deleted
            await _returnRequestRepository.Update(returnRequest);

            var history = new ReturnRequestHistory
            {
                ReturnRequestId = returnRequest.Id,
                Action = "Deleted",
                Note = "Return request deleted",
                Status = 1
            };

            await _returnRequestHistoryRepository.Create(history);
        }
    }
} 