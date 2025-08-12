using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface ISizeService
    {
        Task<StoreProcedureListResult<Size>> GetList(string keySearch, int status, Pagination pagination);
        Task<Size> GetById(int id);
        Task<Size> Create(Size size);
        Task<Size> Update(Size size);
        Task<List<Size>> GetAll();
        Task<List<Size>> GetByStatus(int status);
        Task<Size> GetByName(string name);
        Task<Size> GetByCode(string code);
        Task<List<Size>> FindAllActive();
    }
} 