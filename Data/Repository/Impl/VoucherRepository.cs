using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDbContext context) : base(context)
        {
        }

        public Voucher FindByCode(string code)
        {
            return _dbSet.FirstOrDefault(v => v.Code == code);
        }

        public List<Voucher> FindAllActive()
        {
            return _dbSet.Where(v => v.Status == 1).ToList();
        }

        public List<Voucher> FindByStatus(int status)
        {
            return _dbSet.Where(v => v.Status == status).ToList();
        }
    }
} 