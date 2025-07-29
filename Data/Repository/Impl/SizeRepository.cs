using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        public SizeRepository(AppDbContext context) : base(context)
        {
        }

        public Size FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(s => s.Code == code);
        }

        public Size FindByName(string name)
        {
            return _dbSet.FirstOrDefault(s => s.Name == name);
        }

        public List<Size> FindByStatus(int status)
        {
            return _dbSet.Where(s => s.Status == status).ToList();
        }

        public List<Size> FindAllActive()
        {
            return _dbSet.Where(s => s.Status == 1).ToList();
        }
    }
} 