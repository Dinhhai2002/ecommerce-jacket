using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IUserService
    {
        void Create(User user);
        User FindOne(int id);
        void Update(User user);
        List<User> GetAll();
        User FindByUsername(string username);
        User FindByEmail(string email, int isGoogle);
        User FindByPhone(string phone);
        User FindByUsernameAndEmail(string username, string email);
        User FindByUsernameAndPassword(string username, string password);
        List<User> FindByIds(List<int> ids);
        List<User> FindAllActive();
    }
} 