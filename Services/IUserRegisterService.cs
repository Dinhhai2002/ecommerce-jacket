using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IUserRegisterService
    {
        Task<StoreProcedureListResult<UserRegister>> GetList(string keySearch, int status, Pagination pagination);
        Task<UserRegister> GetById(int id);
        Task<UserRegister> Create(UserRegister userRegister);
        Task<UserRegister> Update(UserRegister userRegister);
        Task<List<UserRegister>> GetAll();
        Task<List<UserRegister>> GetByStatus(int status);
        Task<UserRegister> GetByUsername(string username);
        Task<UserRegister> GetByEmail(string email);
        Task<UserRegister> GetByPhone(string phone);
        Task<List<UserRegister>> FindAllActive();
    }
} 