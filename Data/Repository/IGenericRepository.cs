using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace webecommerce.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        TEntity FindOne(int id);
        void Update(TEntity entity);
        void Delete(int id);
        IEnumerable<TEntity> GetAll();
        TEntity FindByCondition(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindAllByCondition(Expression<Func<TEntity, bool>> predicate);
    }
} 