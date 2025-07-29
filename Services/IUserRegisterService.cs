using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IUserRegisterService
    {
        void Create(UserRegister userRegister);
        UserRegister FindOne(int id);
        void Update(UserRegister userRegister);
        List<UserRegister> GetAll();
        UserRegister FindByUsernameAndEmail(string username, string email);
        List<UserRegister> FindByStatus(int status);
        List<UserRegister> FindAllActive();
    }
} 