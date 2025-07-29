using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public User FindByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username);
        }

        public User FindByEmail(string email, int isGoogle)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email && u.IsGoogle == isGoogle);
        }

        public User FindByPhone(string phone)
        {
            return _dbSet.FirstOrDefault(u => u.Phone == phone);
        }

        public User FindByUsernameAndEmail(string username, string email)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username && u.Email == email);
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }

        public List<User> FindByIds(List<int> ids)
        {
            return _dbSet.Where(u => ids.Contains(u.Id)).ToList();
        }

        public List<User> FindAllActive()
        {
            return _dbSet.Where(u => u.IsActive == 1).ToList();
        }
    }
} 