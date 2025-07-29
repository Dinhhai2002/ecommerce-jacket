using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IReturnRequestDetailRepository : IGenericRepository<ReturnRequestDetail>
    {
        List<ReturnRequestDetail> FindByReturnRequestId(int returnRequestId);
        List<ReturnRequestDetail> FindByOrderDetailId(int orderDetailId);
        List<ReturnRequestDetail> FindByProductDetailId(int productDetailId);
        List<ReturnRequestDetail> FindByStatus(int status);
        List<ReturnRequestDetail> FindAllActive();
    }
} 