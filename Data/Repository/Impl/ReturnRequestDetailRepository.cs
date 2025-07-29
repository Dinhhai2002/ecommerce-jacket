using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webecommerce.Data.Repository.Impl
{
    public class ReturnRequestDetailRepository : GenericRepository<ReturnRequestDetail>, IReturnRequestDetailRepository
    {
        public ReturnRequestDetailRepository(AppDbContext context) : base(context)
        {
        }

        public List<ReturnRequestDetail> FindByReturnRequestId(int returnRequestId)
        {
            return _dbSet.Where(rrd => rrd.ReturnRequestId == returnRequestId).ToList();
        }

        public List<ReturnRequestDetail> FindByOrderDetailId(int orderDetailId)
        {
            return _dbSet.Where(rrd => rrd.OrderDetailId == orderDetailId).ToList();
        }

        public List<ReturnRequestDetail> FindByProductDetailId(int productDetailId)
        {
            return _dbSet.Where(rrd => rrd.ProductDetailId == productDetailId).ToList();
        }

        public List<ReturnRequestDetail> FindByStatus(int status)
        {
            return _dbSet.Where(rrd => rrd.Status == status).ToList();
        }

        public List<ReturnRequestDetail> FindAllActive()
        {
            return _dbSet.Where(rrd => rrd.Status == 1).ToList();
        }
    }
} 