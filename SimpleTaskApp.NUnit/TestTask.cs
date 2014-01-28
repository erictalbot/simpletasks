using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskApp.NUnit.Controllers;

namespace SimpleTaskApp.NUnit
{
    public class testTask : SimpleTaskData.Repositories.ITaskRepository
    {
        #region delegate 

        public delegate SimpleTaskData.Task GetByIdDelegate(int id);
        public delegate void AddDelegate(SimpleTaskData.Task task);

        #endregion

        #region properties

        private GetByIdDelegate GetByIdRoutine { get; set; }
        private AddDelegate AddRoutine { get; set; }

        #endregion

        #region constructor

        public testTask(GetByIdDelegate getByIdRoutine, AddDelegate addRoutine)
        {
            AddRoutine = addRoutine;
            GetByIdRoutine = getByIdRoutine;
        }

        #endregion 

        public IQueryable<SimpleTaskData.Task> GetAll()
        {
            List<SimpleTaskData.Task> testListOfTask = new List<SimpleTaskData.Task>();
            return testListOfTask.AsQueryable();
        }

        public SimpleTaskData.Task GetById(int id)
        {
            return GetByIdRoutine(id);
        }

        public SimpleTaskData.Task GetByDescription(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(SimpleTaskData.Task entity)
        {
            AddRoutine(entity);
        }

        public void Update(SimpleTaskData.Task entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(SimpleTaskData.Task entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
           return (id == (int)SimpleTaskType.ExistingSimpleTask ? true: false);
        }
    }
}
