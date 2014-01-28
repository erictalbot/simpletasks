using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleTaskApp.Controllers.Logic;


namespace SimpleTaskApp.NUnit.Controllers.Logic
{
    [TestFixture]
    public class TaskManagerTest
    {
        [TestFixtureSetUp] // Runs once before any of the tests
        public void TestFixtureSetUp()
        {
        }

        [SetUp] // Runs before every tests
        public void SetUp()
        {
        }

        [TearDown] // Runs after every tests
        public void TearDown()
        {
        }

        [TestFixtureTearDown] // runs once after every tests are completed.
        public void TestFixtureTearDown()
        {
        }

        [Test]
        public void PostWillCallCommit()
        {
            bool commitCalled = false;
            testUnitOfWork.CommitDelegate commitFunction = delegate() { commitCalled = true; };
            TaskManager t = new TaskManager(new testUnitOfWork(commitFunction));
            t.PostTask(new SimpleTaskData.Task());
            Assert.That(commitCalled, "Posting a new Task does not call Commit");
        }

        [Test]
        public void PostWillCallGetById()
        {
            bool getByIdCalled = false;

            testTask.GetByIdDelegate getbyIdFunction = delegate(int id)
                {
                    getByIdCalled = true;
                    return new SimpleTaskData.Task();
                };

            TaskManager t = new TaskManager(new testUnitOfWork(getbyIdFunction));
            t.PostTask(new SimpleTaskData.Task());
            Assert.That(getByIdCalled, "Posting a new Task does not call GetById");
        }

        [Test]
        public void AddIsCalledWhenGetByIdReturnsNull()
        {
            testTask.GetByIdDelegate getbyIdFunction = delegate(int id)
            {
                return null;
            };

            bool addCalled = false;
            testTask.AddDelegate addFunction = delegate(SimpleTaskData.Task task)
            {
                addCalled = true;
            };

            TaskManager t = new TaskManager(new testUnitOfWork(getbyIdFunction, addFunction));
            t.PostTask(new SimpleTaskData.Task());
            Assert.IsTrue(addCalled,"Add was not called even though GetById returned null.");
        }

        [Test]
        public void AddIsNotCalledWhenGetByIdDoesNotReturnNull()
        {
            testTask.GetByIdDelegate getbyIdFunction = delegate(int id)
            {
                return new SimpleTaskData.Task();
            };

            bool addCalled = false;
            testTask.AddDelegate addFunction = delegate(SimpleTaskData.Task task)
            {
                addCalled = true;
            };

            TaskManager t = new TaskManager(new testUnitOfWork(getbyIdFunction, addFunction));
            t.PostTask(new SimpleTaskData.Task());
            Assert.IsFalse(addCalled, "Add was called even though GetById returned a Task.");
        }
    }
}
