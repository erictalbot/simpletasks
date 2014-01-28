using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskData.Repositories;

namespace SimpleTaskData
{
    /// <summary>
    /// The SimpleTask "Unit of Work"
    ///     1) decouples the repos from the controllers
    ///     2) decouples the DbContext and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type such as a <see cref="Person"/>.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data layer (which is the EF DbContext).
    /// </remarks>
    public class SimpleTaskUow : ISimpleTaskUow, IDisposable
    {
        private SimpleTaskDbContext DbContext { get; set; }
        protected IRepositoryProvider RepositoryProvider { get; set; }
        public ITaskRepository Task { get { return GetRepo<ITaskRepository>(); } }

        #region ctor

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="repositoryProvider"></param>
        public SimpleTaskUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext(null);
            SetRepositoryProvider(repositoryProvider);
        }

        /// <summary>
        /// Contructor usefull for unit testing
        /// </summary>
        /// <param name="repositoryProvider"></param>
        /// <param name="dbContext"></param>
        public SimpleTaskUow(IRepositoryProvider repositoryProvider, SimpleTaskDbContext dbContext)
        {
            CreateDbContext(dbContext);
            SetRepositoryProvider(repositoryProvider);
        }

        #endregion

        private void SetRepositoryProvider(IRepositoryProvider repositoryProvider)
        {
            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }
     
        protected void CreateDbContext(SimpleTaskDbContext dbContext)
        {
            DbContext = (dbContext == null ? new SimpleTaskDbContext() : dbContext);

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            DbContext.Commit();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
