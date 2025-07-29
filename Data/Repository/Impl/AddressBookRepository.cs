using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class AddressBookRepository : GenericRepository<AddressBook>, IAddressBookRepository
    {
        public AddressBookRepository(AppDbContext context) : base(context)
        {
        }

        public List<AddressBook> FindByUserId(int userId)
        {
            return _dbSet.Where(a => a.UserId == userId).ToList();
        }

        public AddressBook FindDefaultByUserId(int userId)
        {
            return _dbSet.FirstOrDefault(a => a.UserId == userId && a.IsDefault == 1);
        }

        public List<AddressBook> FindByStatus(int status)
        {
            return _dbSet.Where(a => a.Status == status).ToList();
        }

        public List<AddressBook> FindByUserIdAndStatus(int userId, int status)
        {
            return _dbSet.Where(a => a.UserId == userId && a.Status == status).ToList();
        }
    }
} 