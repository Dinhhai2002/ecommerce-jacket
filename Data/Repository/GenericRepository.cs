using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }

        protected async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        protected async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        protected List<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        protected TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbSet.Where(e => ids.Contains(EF.Property<int>(e, "Id"))).ToListAsync();
        }

        public virtual async Task<StoreProcedureListResult<TEntity>> GetListAsync(string searchKey = "", int status = 1, Pagination pagination = null)
        {
            var query = _dbSet.AsQueryable();

            // Apply status filter if status is not -1 (all)
            if (status != -1)
            {
                query = query.Where(e => EF.Property<int>(e, "Status") == status);
            }

            // Apply search if searchKey is provided
            if (!string.IsNullOrEmpty(searchKey))
            {
                // This is a basic implementation. Override in specific repositories for custom search logic
                query = query.Where(e => EF.Property<string>(e, "Name").Contains(searchKey));
            }

            // Get total count
            var totalRecords = await query.CountAsync();

            // Apply pagination if provided
            if (pagination != null)
            {
                query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            }

            // Execute query
            var items = await query.ToListAsync();

            return new StoreProcedureListResult<TEntity>
            {
                Items = items,
                TotalRecords = totalRecords,
                StatusCode = 200,
                Message = "Success"
            };
        }
    }
} 