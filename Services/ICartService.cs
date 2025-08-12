using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface ICartService
    {
        Task<StoreProcedureListResult<Cart>> GetList(int userId, string keySearch, int status, Pagination pagination);
        Task<Cart> GetById(int id);
        Task<Cart> Create(Cart cart);
        Task<Cart> Update(Cart cart);
        Task<List<Cart>> GetAll();
        Task<List<Cart>> GetByUserId(int userId);
        Task<List<Cart>> GetByStatus(int status);
    }
} 