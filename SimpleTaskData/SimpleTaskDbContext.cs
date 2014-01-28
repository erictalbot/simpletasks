using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SimpleTaskData
{
    public class SimpleTaskDbContext : DbContext
    {
        public SimpleTaskDbContext() : base(nameOrConnectionString: "SimpleTaskEntities") { }

        public DbSet<Task> Task { get; set; }

        public virtual int Commit()
        {
            return SaveChanges();
        }

    }
}
