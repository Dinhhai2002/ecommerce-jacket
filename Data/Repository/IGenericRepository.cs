using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids);
        Task<StoreProcedureListResult<TEntity>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null);
    }
} 