using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Models;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<StoreProcedureListResult<Size>> GetList(string keySearch, int status, Pagination pagination)
        {
            return await _sizeRepository.SpGListSize(keySearch, status, pagination);
        }

        public async Task<Size> GetById(int id)
        {
            return await _sizeRepository.FindOne(id);
        }

        public async Task<Size> Create(Size size)
        {
            await _sizeRepository.Create(size);
            return size;
        }

        public async Task<Size> Update(Size size)
        {
            await _sizeRepository.Update(size);
            return size;
        }

        public async Task<List<Size>> GetAll()
        {
            return await _sizeRepository.GetAll().ToListAsync();
        }

        public async Task<List<Size>> GetByStatus(int status)
        {
            return await _sizeRepository.FindByCondition(s => s.Status == status).ToListAsync();
        }

        public async Task<Size> GetByName(string name)
        {
            return await _sizeRepository.FindByName(name);
        }

        public async Task<Size> GetByCode(string code)
        {
            return await _sizeRepository.FindByCode(code);
        }

        public async Task<List<Size>> FindAllActive()
        {
            return await _sizeRepository.FindAllActive();
        }
    }
} 