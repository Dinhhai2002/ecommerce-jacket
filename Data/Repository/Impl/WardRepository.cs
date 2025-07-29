using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class WardRepository : GenericRepository<Wards>, IWardRepository
    {
        public WardRepository(AppDbContext context) : base(context)
        {
        }

        public Wards FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(w => w.Code == code);
        }

        public List<Wards> FindByDistrictId(int districtId)
        {
            return _dbSet.Where(w => w.DistrictId == districtId).ToList();
        }

        public List<Wards> FindByStatus(int status)
        {
            return _dbSet.Where(w => w.Status == status).ToList();
        }

        public List<Wards> FindAllActive()
        {
            return _dbSet.Where(w => w.Status == 1).ToList();
        }
    }
} 