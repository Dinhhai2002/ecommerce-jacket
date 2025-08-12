using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IBannerService
    {
        Task<StoreProcedureListResult<Banner>> GetList(string keySearch, int status, Pagination pagination);
        Task<Banner> GetById(int id);
        Task<Banner> Create(Banner banner);
        Task<Banner> Update(Banner banner);
        Task<List<Banner>> GetAll();
        Task<List<Banner>> GetByStatus(int status);
        Task<Banner> GetByName(string name);
        Task<List<Banner>> FindAllActive();
    }
} 