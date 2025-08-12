using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IVoucherService
    {
        Task<StoreProcedureListResult<Voucher>> GetList(string keySearch, int status, Pagination pagination);
        Task<Voucher> GetById(int id);
        Task<Voucher> Create(Voucher voucher);
        Task<Voucher> Update(Voucher voucher);
        Task<List<Voucher>> GetAll();
        Task<List<Voucher>> GetByStatus(int status);
        Task<Voucher> GetByCode(string code);
        Task<decimal> CalculateDiscount(Voucher voucher, decimal amount);
    }
} 