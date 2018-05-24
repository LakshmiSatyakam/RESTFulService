using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RESTFulService.Entity
{
    /// <summary>
    /// Class to perform operations on an Entity
    /// </summary>
    /// <typeparam name="T">Class type implementing IEntityBase</typeparam>
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        #region Private fields
        private readonly MembersContext _context; 
        #endregion

        #region Properties
        public EntityBaseRepository(MembersContext context)
        {
            _context = context;
        }
        #endregion

        #region IEntityBaseRepository implementation
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public T GetSingle(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        } 
        #endregion
    }
}
