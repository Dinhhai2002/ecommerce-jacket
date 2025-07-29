using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IReturnRequestDetailService
    {
        void Create(ReturnRequestDetail returnRequestDetail);
        ReturnRequestDetail FindOne(int id);
        void Update(ReturnRequestDetail returnRequestDetail);
        List<ReturnRequestDetail> GetAll();
        List<ReturnRequestDetail> FindByReturnRequestId(int returnRequestId);
        List<ReturnRequestDetail> FindByOrderDetailId(int orderDetailId);
        List<ReturnRequestDetail> FindByProductDetailId(int productDetailId);
        List<ReturnRequestDetail> FindByStatus(int status);
        List<ReturnRequestDetail> FindAllActive();
    }
} 