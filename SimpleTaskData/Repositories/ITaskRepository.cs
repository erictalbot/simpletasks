using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskData.Repositories
{
    /// <summary>
    /// Methods that a task repository would provide over a usual IRepository
    /// </summary>
    public interface  ITaskRepository : IRepository<Task>
    {

    }
}
