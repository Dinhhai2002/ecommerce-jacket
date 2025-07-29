using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IExchangeRequestDetailRepository : IGenericRepository<ExchangeRequestDetail>
    {
        List<ExchangeRequestDetail> FindByExchangeRequestId(int exchangeRequestId);
        List<ExchangeRequestDetail> FindByOrderDetailId(int orderDetailId);
        List<ExchangeRequestDetail> FindByProductDetailId(int productDetailId);
        List<ExchangeRequestDetail> FindByExchangeProductDetailId(int exchangeProductDetailId);
        List<ExchangeRequestDetail> FindByStatus(int status);
        List<ExchangeRequestDetail> FindAllActive();
    }
} 