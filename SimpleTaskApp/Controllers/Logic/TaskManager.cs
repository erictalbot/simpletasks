using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleTaskData;


namespace SimpleTaskApp.Controllers.Logic
{
    /// <summary>
    /// Encapsulate the logic of Add/Modifying a task. For example, if the SimpleTaskId is already use, modify it.
    /// Other implementation could check that task has a description, a due date ...
    /// </summary>
    public class TaskManager
    {
        protected ISimpleTaskUow Uow { get; set; }

        public TaskManager(ISimpleTaskUow uow)
        {
            Uow = uow;   
        }

        public Task PostTask(Task task)
        {
            Task taskToUpdate = Uow.Task.GetById(task.SimpleTaskId);
            if (taskToUpdate != null)
            {
                taskToUpdate.Description = task.Description;
                taskToUpdate.DueDate = task.DueDate;
            }
            else
                Uow.Task.Add(task);
            Uow.Commit();       // Reminder : the commit automatically updates the identifier : SimpleTaskId
            return task;
        } 
    }
}
