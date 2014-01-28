using System;
using System.Data.Entity;
using System.Linq;
using SimpleTaskData;


namespace SimpleTaskData.Repositories
{
    /// <summary>
    /// TaskRepository override the DefaultRepository to provide only methods that Tasks needs (Override and the throw exception )
    /// and other that DefaultRepository would not provide.
    /// </summary>
    public class TaskRepository : DefaultRepository<Task>, ITaskRepository
    {
        public TaskRepository(DbContext context) : base(context) { }

        public override IQueryable<Task> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// This is a reason why we gotta TaskRepository, the DefaultRepository does not implement GetByDescription.
        /// Could be used by a search.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task GetByDescription(string id)
        {
            return (from t in DbSet where t.Description == id select t).FirstOrDefault();
        }
    }
}
