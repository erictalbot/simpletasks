using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleTaskData;
using SimpleTaskApp.Models.Views;

namespace SimpleTaskApp.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(ISimpleTaskUow uow)
        {
            Uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            TaskFormViewModel model = new TaskFormViewModel
            {
                VerbName = "Post",
                ControllerName = "Task",
                DueDate = DateTime.Today.ToString()
            };
            return PartialView("CreateTaskForm", model);
        }

        [HttpGet]
        public ActionResult EditTask(int id)
        {
            TaskFormViewModel model = new TaskFormViewModel(Uow.Task.GetById(id)) { VerbName = "Post", ControllerName = "Task" };
            return PartialView("EditTaskForm", model);
        }
    }
}

