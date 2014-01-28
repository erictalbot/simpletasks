using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleTaskApp.Controllers;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
 using System.Web.Script.Serialization;

namespace SimpleTaskApp.NUnit.Controllers
{

    public enum SimpleTaskType
    {
        ExistingSimpleTask = 1,
        InexistingSimpleTask = 2
    }

    [TestFixture]
    public class TaskControllerTests
    {
        [TestFixtureSetUp] // Runs once before any of the tests
        public void TestFixtureSetUp()
        {
        }

        [SetUp] // Runs before every tests
        public void SetUp()
        {
            taskController = new TaskController(new testUnitOfWork());
        }

        [TearDown] // Runs after every tests
        public void TearDown()
        {
        }

        [TestFixtureTearDown] // runs once after every tests are completed.
        public void TestFixtureTearDown()
        {
        }

        #region properties

        private TaskController taskController { get; set; }

        #endregion 

        [Test]
        public void GetReturnIEnumerableOfTask()
        {
            IEnumerable<SimpleTaskData.Task> allTask = taskController.Get();
            Assert.That(allTask != null, "Get should return IEnumerable<SimpleTaskData.Task>");
        }

        [Test]
        public void DeleteShouldReturnHTTPStatusOKWhenSimpleTaskExist() 
        {
            testUnitOfWork.CommitDelegate testFunction = delegate() {  };
            taskController = new TaskController(new testUnitOfWork(testFunction));
            HttpRequestMessage fakeRequest = new HttpRequestMessage();
            taskController.Request = fakeRequest;
            HttpResponseMessage response = taskController.Delete((int)SimpleTaskType.ExistingSimpleTask);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK, "Should return Ok when task exist.");
        }

        [Test]
        public void DeleteShouldReturnHTTPStatusNotFoundWhenSimpleTaskDoesNotExist()
        {
            HttpRequestMessage fakeRequest = new HttpRequestMessage();
            taskController.Request = fakeRequest;
            HttpResponseMessage response = taskController.Delete((int)SimpleTaskType.InexistingSimpleTask);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound, "Should return Not Found when task does not exist.");
        }

        [Test]
        public void DeleteShouldCommitWhenTaskExist()
        {
            bool commitCalled = false;
            testUnitOfWork.CommitDelegate testFunction = delegate() { commitCalled = true; };
            taskController = new TaskController(new testUnitOfWork(testFunction));
            HttpRequestMessage fakeRequest = new HttpRequestMessage();
            taskController.Request = fakeRequest;
            HttpResponseMessage response = taskController.Delete((int)SimpleTaskType.ExistingSimpleTask);
            Assert.That(commitCalled, "UnitOfWork commit should have been called.");
        }

        [Test]
        public void PostReturnsHttpStatusCodeOK()
        {
            testUnitOfWork.CommitDelegate testFunction = delegate() { };
            taskController = new TaskController(new testUnitOfWork(testFunction));
            HttpRequestMessage fakeRequest = new HttpRequestMessage();
            taskController.Request = fakeRequest;
            taskController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            HttpResponseMessage response = taskController.Post(new Models.Views.TaskFormViewModel());
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK, "Should return Ok.");
        }

        [Test]
        public void PostReturnsCreatedTask()
        {
            testUnitOfWork.CommitDelegate testFunction = delegate() { };
            taskController = new TaskController(new testUnitOfWork(testFunction));
            HttpRequestMessage fakeRequest = new HttpRequestMessage();
            taskController.Request = fakeRequest;
            taskController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            HttpResponseMessage response = taskController.Post(new Models.Views.TaskFormViewModel());
            Assert.That(response.Content != null, "Response should contain a Task object.");
            Assert.That(response.Content.ReadAsStringAsync().Result.Contains("SimpleTaskId"), "Response should contain a Task object.");         
        }
    }
}
