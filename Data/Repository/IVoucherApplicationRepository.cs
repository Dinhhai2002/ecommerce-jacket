using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IVoucherApplicationRepository : IGenericRepository<VoucherApplication>
    {
        List<VoucherApplication> FindByVoucherId(int voucherId);
        List<VoucherApplication> FindByUserId(int userId);
        List<VoucherApplication> FindByOrderId(int orderId);
        VoucherApplication FindByOrderIdAndVoucherId(int orderId, int voucherId);
        List<VoucherApplication> FindByStatus(int status);
        List<VoucherApplication> FindAllActive();
    }
} 