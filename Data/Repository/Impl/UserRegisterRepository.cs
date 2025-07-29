using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class UserRegisterRepository : GenericRepository<UserRegister>, IUserRegisterRepository
    {
        public UserRegisterRepository(AppDbContext context) : base(context)
        {
        }

        public UserRegister FindByUsernameAndEmail(string username, string email)
        {
            return _dbSet.FirstOrDefault(ur => ur.Username == username && ur.Email == email);
        }

        public List<UserRegister> FindByStatus(int status)
        {
            return _dbSet.Where(ur => ur.Status == status).ToList();
        }

        public List<UserRegister> FindAllActive()
        {
            return _dbSet.Where(ur => ur.Status == 1).ToList();
        }
    }
} 