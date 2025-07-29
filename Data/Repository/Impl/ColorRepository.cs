using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context)
        {
        }

        public Color FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(c => c.Code == code);
        }

        public Color FindByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Name == name);
        }

        public List<Color> FindByStatus(int status)
        {
            return _dbSet.Where(c => c.Status == status).ToList();
        }

        public List<Color> FindAllActive()
        {
            return _dbSet.Where(c => c.Status == 1).ToList();
        }
    }
} 