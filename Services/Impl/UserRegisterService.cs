using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUserRegisterRepository _userRegisterRepository;

        public UserRegisterService(IUserRegisterRepository userRegisterRepository)
        {
            _userRegisterRepository = userRegisterRepository;
        }

        public void Create(UserRegister userRegister)
        {
            _userRegisterRepository.Create(userRegister);
        }

        public UserRegister FindOne(int id)
        {
            return _userRegisterRepository.FindOne(id);
        }

        public void Update(UserRegister userRegister)
        {
            _userRegisterRepository.Update(userRegister);
        }

        public List<UserRegister> GetAll()
        {
            return _userRegisterRepository.GetAll();
        }

        public UserRegister FindByUsernameAndEmail(string username, string email)
        {
            return _userRegisterRepository.FindByUsernameAndEmail(username, email);
        }

        public List<UserRegister> FindByStatus(int status)
        {
            return _userRegisterRepository.FindByStatus(status);
        }

        public List<UserRegister> FindAllActive()
        {
            return _userRegisterRepository.FindAllActive();
        }
    }
} 