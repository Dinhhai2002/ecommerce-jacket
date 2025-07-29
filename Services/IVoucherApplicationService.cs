using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IVoucherApplicationService
    {
        void Create(VoucherApplication voucherApplication);
        VoucherApplication FindOne(int id);
        void Update(VoucherApplication voucherApplication);
        List<VoucherApplication> GetAll();
        List<VoucherApplication> FindByVoucherId(int voucherId);
        List<VoucherApplication> FindByUserId(int userId);
        List<VoucherApplication> FindByOrderId(int orderId);
        VoucherApplication FindByOrderIdAndVoucherId(int orderId, int voucherId);
        List<VoucherApplication> FindByStatus(int status);
        List<VoucherApplication> FindAllActive();
    }
} 