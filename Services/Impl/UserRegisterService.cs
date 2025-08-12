using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUserRegisterRepository _userRegisterRepository;

        public UserRegisterService(IUserRegisterRepository userRegisterRepository)
        {
            _userRegisterRepository = userRegisterRepository;
        }

        public async Task<StoreProcedureListResult<UserRegister>> GetList(string keySearch, int status, Pagination pagination)
        {
            return await _userRegisterRepository.SpGListUserRegister(keySearch, status, pagination);
        }

        public async Task<UserRegister> GetById(int id)
        {
            return await _userRegisterRepository.FindOne(id);
        }

        public async Task<UserRegister> Create(UserRegister userRegister)
        {
            await _userRegisterRepository.Create(userRegister);
            return userRegister;
        }

        public async Task<UserRegister> Update(UserRegister userRegister)
        {
            await _userRegisterRepository.Update(userRegister);
            return userRegister;
        }

        public async Task<List<UserRegister>> GetAll()
        {
            return await _userRegisterRepository.GetAll().ToListAsync();
        }

        public async Task<List<UserRegister>> GetByStatus(int status)
        {
            return await _userRegisterRepository.FindByCondition(u => u.Status == status).ToListAsync();
        }

        public async Task<UserRegister> GetByUsername(string username)
        {
            return await _userRegisterRepository.FindByUsername(username);
        }

        public async Task<UserRegister> GetByEmail(string email)
        {
            return await _userRegisterRepository.FindByEmail(email);
        }

        public async Task<UserRegister> GetByPhone(string phone)
        {
            return await _userRegisterRepository.FindByPhone(phone);
        }

        public async Task<List<UserRegister>> FindAllActive()
        {
            return await _userRegisterRepository.FindAllActive();
        }
    }
} 