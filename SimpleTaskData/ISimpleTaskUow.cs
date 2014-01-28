using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskData.Repositories;

namespace SimpleTaskData
{
    public interface ISimpleTaskUow
    {
        // Save pending changes to the data store.
        void Commit();

        // Repositories
        ITaskRepository Task { get; }
    }
}
