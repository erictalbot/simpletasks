using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskApp.NUnit
{
    public class testUnitOfWork : SimpleTaskData.ISimpleTaskUow
    {
        public delegate void CommitDelegate();
        private CommitDelegate CommitRoutine { get; set; }
        private testTask.GetByIdDelegate GetByIdRoutine { get; set; }
        private testTask.AddDelegate AddRoutine { get; set; }

        #region contructors

        public testUnitOfWork(CommitDelegate commitRoutine, testTask.GetByIdDelegate getByIdRoutine)
        {
            CommitRoutine = commitRoutine;
            GetByIdRoutine = getByIdRoutine;
            testTask.AddDelegate addRoutine = delegate(SimpleTaskData.Task task) { };
            AddRoutine = addRoutine;
        }

        public testUnitOfWork()
        { 
            CommitDelegate commitFunction = delegate() { };
            CommitRoutine = commitFunction;
            testTask.GetByIdDelegate getByIdFunction = delegate(int id) { return new SimpleTaskData.Task(); };
            GetByIdRoutine = getByIdFunction;
            testTask.AddDelegate addRoutine = delegate(SimpleTaskData.Task task) { };
            AddRoutine = addRoutine;
        }

        public testUnitOfWork(CommitDelegate commitRoutine)
        {
            CommitRoutine = commitRoutine;
            testTask.GetByIdDelegate getByIdFunction = delegate(int id) { return new SimpleTaskData.Task(); };
            GetByIdRoutine = getByIdFunction;
            testTask.AddDelegate addRoutine = delegate(SimpleTaskData.Task task) { };
            AddRoutine = addRoutine;
        }

        public testUnitOfWork(testTask.GetByIdDelegate getByIdRoutine)
        {
            CommitDelegate commitFunction = delegate() { };
            CommitRoutine = commitFunction;
            GetByIdRoutine = getByIdRoutine;
            testTask.AddDelegate addRoutine = delegate(SimpleTaskData.Task task) { };
            AddRoutine = addRoutine;

        }

        public testUnitOfWork(testTask.GetByIdDelegate getByIdRoutine, testTask.AddDelegate addRoutine)
        {
            CommitDelegate commitFunction = delegate() { };
            CommitRoutine = commitFunction;
            GetByIdRoutine = getByIdRoutine;
            AddRoutine = addRoutine;
        }

        #endregion

        void SimpleTaskData.ISimpleTaskUow.Commit()
        {
            CommitRoutine();
        }

        SimpleTaskData.Repositories.ITaskRepository SimpleTaskData.ISimpleTaskUow.Task
        {
            get { return new testTask(GetByIdRoutine,AddRoutine); }
        }
    }
}
