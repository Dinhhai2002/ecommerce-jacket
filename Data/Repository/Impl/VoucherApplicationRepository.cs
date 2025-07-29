using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class VoucherApplicationRepository : GenericRepository<VoucherApplication>, IVoucherApplicationRepository
    {
        public VoucherApplicationRepository(AppDbContext context) : base(context)
        {
        }

        public List<VoucherApplication> FindByVoucherId(int voucherId)
        {
            return _dbSet.Where(va => va.VoucherId == voucherId).ToList();
        }

        public List<VoucherApplication> FindByUserId(int userId)
        {
            return _dbSet.Where(va => va.UserId == userId).ToList();
        }

        public List<VoucherApplication> FindByOrderId(int orderId)
        {
            return _dbSet.Where(va => va.OrderId == orderId).ToList();
        }

        public VoucherApplication FindByOrderIdAndVoucherId(int orderId, int voucherId)
        {
            return _dbSet.FirstOrDefault(va => va.OrderId == orderId && va.VoucherId == voucherId);
        }

        public List<VoucherApplication> FindByStatus(int status)
        {
            return _dbSet.Where(va => va.Status == status).ToList();
        }

        public List<VoucherApplication> FindAllActive()
        {
            return _dbSet.Where(va => va.Status == 1).ToList();
        }
    }
} 