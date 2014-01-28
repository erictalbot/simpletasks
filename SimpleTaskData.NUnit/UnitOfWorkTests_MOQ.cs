using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskData;
using NUnit.Framework;
using SimpleTaskData.Repositories;
using System.Data.Entity;
using SimpleTaskData.NUnit.Repositories;
using Moq;

namespace SimpleTaskData.NUnit
{
    public class UnitOfWorkTests
    {
        #region properties

        RepositoryProvider RepoProvider { get; set; }

        #endregion

        /// <summary>
        /// This explains how MOQ works.
        /// </summary>
        [Test]
        public void CallingCommit_will_call_DbcontextCommit()
        {
            // Arrange
            IDictionary<Type, Func<DbContext, object>> _repositoryFactories = CreateTestFactories();
            RepositoryFactories repoFactory = new RepositoryFactories(_repositoryFactories);
            RepoProvider = new RepositoryProvider(repoFactory);
            var mockDbContext = new Mock<SimpleTaskDbContext>();
            mockDbContext.Setup(x => x.Commit());           //SimpleTaskDbContext.Commit is virtual because we're not using an interface.
            SimpleTaskData.SimpleTaskUow uow = new SimpleTaskUow(RepoProvider, mockDbContext.Object);

            // Act
            uow.Commit();

            // Assert
            mockDbContext.VerifyAll();
        }

     
        #region utility method

        /// <summary>
        /// Create a dictionary to store test factory function
        /// </summary>
        /// <returns></returns>
        private IDictionary<Type, Func<DbContext, object>> CreateTestFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                     {typeof(ITestRepository), DbContext => new TestRepository()} 
                };
        }

        #endregion

    }
}
