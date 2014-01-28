using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace $rootnamespace$.Data.Repositories.Interfaces
{
    /// <summary>
    /// Generic Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(string id);
        void Delete(Func<T, Boolean> predicate);
        T Get(Func<T, Boolean> where);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetMany(Func<T, bool> where);

        T Find(string id);

        void Commit();
    }
}