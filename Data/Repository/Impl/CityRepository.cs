using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class CityRepository : GenericRepository<Cities>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public Cities FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(c => c.Code == code);
        }

        public List<Cities> FindByStatus(int status)
        {
            return _dbSet.Where(c => c.Status == status).ToList();
        }

        public List<Cities> FindAllActive()
        {
            return _dbSet.Where(c => c.Status == 1).ToList();
        }
    }
} 