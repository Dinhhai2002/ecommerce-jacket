using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class ReturnRequestDetailService : IReturnRequestDetailService
    {
        private readonly IReturnRequestDetailRepository _returnRequestDetailRepository;

        public ReturnRequestDetailService(IReturnRequestDetailRepository returnRequestDetailRepository)
        {
            _returnRequestDetailRepository = returnRequestDetailRepository;
        }

        public void Create(ReturnRequestDetail returnRequestDetail)
        {
            _returnRequestDetailRepository.Create(returnRequestDetail);
        }

        public ReturnRequestDetail FindOne(int id)
        {
            return _returnRequestDetailRepository.FindOne(id);
        }

        public void Update(ReturnRequestDetail returnRequestDetail)
        {
            _returnRequestDetailRepository.Update(returnRequestDetail);
        }

        public List<ReturnRequestDetail> GetAll()
        {
            return _returnRequestDetailRepository.GetAll();
        }

        public List<ReturnRequestDetail> FindByReturnRequestId(int returnRequestId)
        {
            return _returnRequestDetailRepository.FindByReturnRequestId(returnRequestId);
        }

        public List<ReturnRequestDetail> FindByOrderDetailId(int orderDetailId)
        {
            return _returnRequestDetailRepository.FindByOrderDetailId(orderDetailId);
        }

        public List<ReturnRequestDetail> FindByProductDetailId(int productDetailId)
        {
            return _returnRequestDetailRepository.FindByProductDetailId(productDetailId);
        }

        public List<ReturnRequestDetail> FindByStatus(int status)
        {
            return _returnRequestDetailRepository.FindByStatus(status);
        }

        public List<ReturnRequestDetail> FindAllActive()
        {
            return _returnRequestDetailRepository.FindAllActive();
        }
    }
} 