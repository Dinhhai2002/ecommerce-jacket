using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class MaterialsRepository : GenericRepository<Materials>, IMaterialsRepository
    {
        public MaterialsRepository(AppDbContext context) : base(context)
        {
        }

        public Materials FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(m => m.Code == code);
        }

        public Materials FindByName(string name)
        {
            return _dbSet.FirstOrDefault(m => m.Name == name);
        }

        public List<Materials> FindByStatus(int status)
        {
            return _dbSet.Where(m => m.Status == status).ToList();
        }

        public List<Materials> FindAllActive()
        {
            return _dbSet.Where(m => m.Status == 1).ToList();
        }
    }
} 