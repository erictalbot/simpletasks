using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskApp.Controllers;
using System.Web.Mvc;
using NUnit.Framework;

namespace SimpleTaskApp.NUnit.Controllers
{
   [TestFixture]
    public class HomeControllerTests
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
        public void CreateTask_ShouldReturnATaskFormViewModel()
        {
            bool exceptionThrown =false;
            SimpleTaskApp.Models.Views.TaskFormViewModel taskFormViewModel = null;
            HomeController h = new HomeController(new testUnitOfWork());
            ActionResult result = h.CreateTask();
            try
            {
                taskFormViewModel = ((System.Web.Mvc.ViewResultBase)(result)).Model as SimpleTaskApp.Models.Views.TaskFormViewModel;
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }
            Assert.That(exceptionThrown == false, "No exception should have been throwns while casting as SimpleTaskApp.Models.Views.TaskFormViewModel.");
            Assert.That(taskFormViewModel != null, "A new  SimpleTaskApp.Models.Views.TaskFormViewModel should have been returned.");
            Assert.IsNull(taskFormViewModel.Description, "Description of new TaskFormViewModel should be null");
            Assert.That(taskFormViewModel.SimpleTaskId==0, "SimpleTaskId should be 0 on a new TaskFormViewModel");
            Assert.That(taskFormViewModel.VerbName=="Post", "VerbName should be POST on a new TaskFormViewModel");
            Assert.That(taskFormViewModel.ControllerName == "Task", "ControllerName should be TASK on any TaskFormViewModel returned");
        }

       [Test]
       public void EditTask_ShouldReturnATaskFormViewModel()
       {
           bool exceptionThrown = false;
           SimpleTaskApp.Models.Views.TaskFormViewModel taskFormViewModel = null;
           HomeController h = new HomeController(new testUnitOfWork());
           ActionResult result = h.EditTask((int)SimpleTaskType.ExistingSimpleTask);
           try
           {
               taskFormViewModel = ((System.Web.Mvc.ViewResultBase)(result)).Model as SimpleTaskApp.Models.Views.TaskFormViewModel;
           }
           catch (Exception)
           {
               exceptionThrown = true;
           }
           Assert.That(exceptionThrown == false, "No exception should have been throwns while casting as SimpleTaskApp.Models.Views.TaskFormViewModel.");
           Assert.That(taskFormViewModel != null, "A new  SimpleTaskApp.Models.Views.TaskFormViewModel should have been returned.");
           Assert.That(taskFormViewModel.VerbName == "Post", "VerbName should be POST on any TaskFormViewModel returned");
           Assert.That(taskFormViewModel.ControllerName == "Task", "ControllerName should be TASK on any TaskFormViewModel returned");
       }

       [Test]
       public void EditTask_ShouldReturnATaskFormViewModel_EvenIfTaskDoesNotExist()
       {
           bool exceptionThrown = false;
           SimpleTaskApp.Models.Views.TaskFormViewModel taskFormViewModel = null;
           HomeController h = new HomeController(new testUnitOfWork());
           ActionResult result = h.EditTask((int)SimpleTaskType.InexistingSimpleTask);
           try
           {
               taskFormViewModel = ((System.Web.Mvc.ViewResultBase)(result)).Model as SimpleTaskApp.Models.Views.TaskFormViewModel;
           }
           catch (Exception)
           {
               exceptionThrown = true;
           }
           Assert.That(exceptionThrown == false, "No exception should have been throwns while casting as SimpleTaskApp.Models.Views.TaskFormViewModel.");
           Assert.That(taskFormViewModel != null, "A new  SimpleTaskApp.Models.Views.TaskFormViewModel should have been returned.");
           Assert.That(taskFormViewModel.VerbName == "Post", "VerbName should be POST on any TaskFormViewModel returned");
           Assert.That(taskFormViewModel.ControllerName == "Task", "ControllerName should be TASK on any TaskFormViewModel returned");
       }

    }
}
