using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleTaskData;
using SimpleTaskApp.Models.Views;
using SimpleTaskApp.Controllers.Logic;

namespace SimpleTaskApp.Controllers
{
    public class TaskController : ApiControllerBase
    {
        public TaskController(ISimpleTaskUow uow)
        {
            Uow = uow;
        }

        // GET api/task
        public IEnumerable<Task> Get()
        {
            return Uow.Task.GetAll().OrderBy(p => p.Description);
        }

        // DELETE api/task/id where id is a int
        public HttpResponseMessage Delete(int id)
        {
            if (Uow.Task.Delete(id))
            {
                Uow.Commit();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/task
        public HttpResponseMessage Post(TaskFormViewModel tFVM)
        {
            TaskManager tM = new TaskManager(Uow);
            Task createdTask = tM.PostTask(tFVM.Task);
            return Request.CreateResponse(HttpStatusCode.OK, createdTask);
        }
    }
}
