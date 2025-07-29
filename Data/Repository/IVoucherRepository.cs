using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IVoucherRepository : IGenericRepository<Voucher>
    {
        Voucher FindByCode(string code);
        List<Voucher> FindAllActive();
        List<Voucher> FindByStatus(int status);
    }
} 