using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public interface IUserRegisterRepository : IGenericRepository<UserRegister>
    {
        Task<StoreProcedureListResult<UserRegister>> SpGListUserRegister(string keySearch, int status, Pagination pagination);
        Task<UserRegister> FindByUsername(string username);
        Task<UserRegister> FindByEmail(string email);
        Task<UserRegister> FindByPhone(string phone);
        Task<List<UserRegister>> FindByStatus(int status);
        Task<List<UserRegister>> FindAllActive();
    }
} 