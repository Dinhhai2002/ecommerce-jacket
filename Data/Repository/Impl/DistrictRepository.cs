using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class DistrictRepository : GenericRepository<Districts>, IDistrictRepository
    {
        public DistrictRepository(AppDbContext context) : base(context)
        {
        }

        public Districts FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(d => d.Code == code);
        }

        public List<Districts> FindByCityId(int cityId)
        {
            return _dbSet.Where(d => d.CityId == cityId).ToList();
        }

        public List<Districts> FindByStatus(int status)
        {
            return _dbSet.Where(d => d.Status == status).ToList();
        }

        public List<Districts> FindAllActive()
        {
            return _dbSet.Where(d => d.Status == 1).ToList();
        }
    }
} 