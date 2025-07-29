using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindByUsername(string username);
        User FindByEmail(string email, int isGoogle);
        User FindByPhone(string phone);
        User FindByUsernameAndEmail(string username, string email);
        User FindByUsernameAndPassword(string username, string password);
        List<User> FindByIds(List<int> ids);
        List<User> FindAllActive();
    }
} 