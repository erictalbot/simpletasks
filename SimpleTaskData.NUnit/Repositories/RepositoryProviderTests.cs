using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleTaskData.Repositories;
using System.Data.Entity;

namespace SimpleTaskData.NUnit.Repositories
{

    [TestFixture]
    public class RepositoryProviderTests
    {
        #region properties

        RepositoryProvider RepoProvider { get; set; }

        #endregion

        #region setup/teardown

        [TestFixtureSetUp] // Runs once before any of the tests
        public void TestFixtureSetUp()
        {
        }

        [SetUp] // Runs before every tests
        public void SetUp()
        {
            IDictionary<Type, Func<DbContext, object>> _repositoryFactories = CreateTestFactories();
            RepositoryFactories repoFactory = new RepositoryFactories(_repositoryFactories);
            RepoProvider = new RepositoryProvider(repoFactory);
            SimpleTaskDbContext dbContext = new SimpleTaskDbContext();
            RepoProvider.DbContext = dbContext;
        }

        [TearDown] // Runs after every tests
        public void TearDown()
        {
        }

        [TestFixtureTearDown] // runs once after every tests are completed.
        public void TestFixtureTearDown()
        {
        }

        #endregion

        #region private methods

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

        private IDictionary<Type, Func<DbContext, object>> CreateSimpleTaskFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                     {typeof(ITaskRepository), DbContext => new TaskRepository(new SimpleTaskData.SimpleTaskDbContext())} 
                };
        }

        #endregion

        #region tests methods

        [Test]
        public void GetRepositoryForARepositoryTypeThatIsInTheRepositoryFactoriesShouldReturnTheTypeSpecifiedInTheRepositoryFactories()
        {
            var repository  = RepoProvider.GetRepository<ITestRepository>();

            Assert.That(repository.GetType().Name == "TestRepository", "ITestRepository ?");
        }

        [Test]
        public void GetRepositoryForITestUnusedRepositoryShouldThrowException_IfNotInCacheAndNoFunctionProvided()
        {
            bool exceptionThrown = false;
            try
            {
                var repository = RepoProvider.GetRepository<ITestUnusedRepository>();
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.That(exceptionThrown);
        }

        [Test]
        public void GetRepositoryForARepositoryTypeWithFunctionShouldReturnTheRepositorySpecifiedByTheFunction()
        {
            var repository = RepoProvider.GetRepository<ITestUnusedRepository>(DbContext => new TestUnusedRepository());

            Assert.That(repository.GetType().Name == "TestUnusedRepository", "ITestUnusedRepository ?");
        }

        [Test]
        public void GetRepositoryForEntityTypeWhereTypeIsNotInDictionaryShouldReturnADefaultRepository()
        {
            var repository = RepoProvider.GetRepositoryForEntityType<ITestUnusedRepository>();
            Assert.That(repository.GetType().Name == "DefaultRepository`1", "Should have received a DefaultRepository because ITestUnusedRepository has not been associated to TestUnusedRepository using the dictionary.");
        }


        [Test]
        public void SetRepositorySetATypeToARepository()
        {
            RepoProvider.SetRepository<ITestUnusedRepository>(new TestUnusedRepository());

            var repository = RepoProvider.GetRepository<ITestUnusedRepository>();

            Assert.That(repository.GetType().Name == "TestUnusedRepository", "Should have received a TestUnusedRepository because SetRepository did associate ITestUnusedRepository to TestUnusedRepository");
        }


        #endregion

    }
}
