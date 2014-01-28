using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data.Entity;
using $rootnamespace$.Data.Repositories.Interfaces;

namespace $rootnamespace$.Data.Repositories
{
    /// <summary>
    /// Generic Repository which can be used in Services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositorySingleton<T> : IRepository<T> where T : class
    {
        private readonly Func<DbContext> _dbContextLocator;

        private IDbSet<T> dbset
        {
            get
            {
                return _dbContextLocator().Set<T>();
            }
        }

        public RepositorySingleton(Func<DbContext> dbContextLocator)
        {
            _dbContextLocator = dbContextLocator;
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(string id)
        {
            var entity = dbset.Find(id);
            dbset.Remove(entity);
        }

        public virtual void Delete(Func<T, Boolean> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }


        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbset;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return dbset.Where(where).ToList();
        }
        public virtual T Get(Func<T, Boolean> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public virtual T Find(string id)
        {
            return dbset.Find(id);
        }


        public virtual void Commit()
        {
            _dbContextLocator().SaveChanges();
        }
    }
}