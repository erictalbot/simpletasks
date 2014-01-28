using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskData.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T GetByDescription(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Delete(int id);
         
    }
}
