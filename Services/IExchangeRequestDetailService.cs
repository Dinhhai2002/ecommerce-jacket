using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IExchangeRequestDetailService
    {
        void Create(ExchangeRequestDetail exchangeRequestDetail);
        ExchangeRequestDetail FindOne(int id);
        void Update(ExchangeRequestDetail exchangeRequestDetail);
        List<ExchangeRequestDetail> GetAll();
        List<ExchangeRequestDetail> FindByExchangeRequestId(int exchangeRequestId);
        List<ExchangeRequestDetail> FindByOrderDetailId(int orderDetailId);
        List<ExchangeRequestDetail> FindByProductDetailId(int productDetailId);
        List<ExchangeRequestDetail> FindByExchangeProductDetailId(int exchangeProductDetailId);
        List<ExchangeRequestDetail> FindByStatus(int status);
        List<ExchangeRequestDetail> FindAllActive();
    }
} 