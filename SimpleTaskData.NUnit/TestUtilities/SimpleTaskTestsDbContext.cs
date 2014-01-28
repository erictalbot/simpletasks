using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SimpleTaskData.NUnit.TestUtilities
{
    public class SimpleTaskTestsDbContext : DbContext
    {
        public SimpleTaskTestsDbContext() : base(nameOrConnectionString: "SimpleTaskEntitiesTests") { }

        public DbSet<Task> Task { get; set; }

    }
}
