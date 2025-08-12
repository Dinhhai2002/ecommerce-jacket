using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IOrderService
    {
        Task<StoreProcedureListResult<Order>> GetList(int userId, string keySearch, int status, int paymentStatus, int paymentMethod, Pagination pagination);
        Task<Order> GetById(int id);
        Task<Order> Create(Order order);
        Task<Order> Update(Order order);
        Task<List<Order>> GetAll();
        Task<List<Order>> GetByUserId(int userId);
        Task<List<Order>> GetByStatus(int status);
        Task<List<Order>> GetByPaymentStatus(int paymentStatus);
        Task<List<Order>> GetByPaymentMethod(int paymentMethod);
        Task<Order> GetByOrderCode(string orderCode);
    }
} 