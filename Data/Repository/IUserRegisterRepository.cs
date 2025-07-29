using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IUserRegisterRepository : IGenericRepository<UserRegister>
    {
        UserRegister FindByUsernameAndEmail(string username, string email);
        List<UserRegister> FindByStatus(int status);
        List<UserRegister> FindAllActive();
    }
} 