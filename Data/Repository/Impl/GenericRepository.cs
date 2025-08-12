using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository.Impl
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) != null;
        }

        public virtual async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbSet.Where(e => ids.Contains(((dynamic)e).Id)).ToListAsync();
        }

        public virtual async Task<StoreProcedureListResult<T>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchKey))
            {
                // Implement search logic based on entity properties
                // This should be overridden in specific repositories
            }

            query = query.Where(e => ((dynamic)e).Status == status);

            var totalRecords = await query.CountAsync();

            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            var items = await query.ToListAsync();

            return new StoreProcedureListResult<T>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }
    }
} 