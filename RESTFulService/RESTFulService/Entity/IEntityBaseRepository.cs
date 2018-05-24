using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RESTFulService.Entity
{
    /// <summary>
    /// Interface to perform operations on an Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll();
        int Count();
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
    }
}
