using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class VoucherApplicationService : IVoucherApplicationService
    {
        private readonly IVoucherApplicationRepository _voucherApplicationRepository;

        public VoucherApplicationService(IVoucherApplicationRepository voucherApplicationRepository)
        {
            _voucherApplicationRepository = voucherApplicationRepository;
        }

        public void Create(VoucherApplication voucherApplication)
        {
            _voucherApplicationRepository.Create(voucherApplication);
        }

        public VoucherApplication FindOne(int id)
        {
            return _voucherApplicationRepository.FindOne(id);
        }

        public void Update(VoucherApplication voucherApplication)
        {
            _voucherApplicationRepository.Update(voucherApplication);
        }

        public List<VoucherApplication> GetAll()
        {
            return _voucherApplicationRepository.GetAll();
        }

        public List<VoucherApplication> FindByVoucherId(int voucherId)
        {
            return _voucherApplicationRepository.FindByVoucherId(voucherId);
        }

        public List<VoucherApplication> FindByUserId(int userId)
        {
            return _voucherApplicationRepository.FindByUserId(userId);
        }

        public List<VoucherApplication> FindByOrderId(int orderId)
        {
            return _voucherApplicationRepository.FindByOrderId(orderId);
        }

        public VoucherApplication FindByOrderIdAndVoucherId(int orderId, int voucherId)
        {
            return _voucherApplicationRepository.FindByOrderIdAndVoucherId(orderId, voucherId);
        }

        public List<VoucherApplication> FindByStatus(int status)
        {
            return _voucherApplicationRepository.FindByStatus(status);
        }

        public List<VoucherApplication> FindAllActive()
        {
            return _voucherApplicationRepository.FindAllActive();
        }
    }
} 