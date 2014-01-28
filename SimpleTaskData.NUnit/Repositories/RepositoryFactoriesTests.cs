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
    public class RepositoryFactoriesTests
    {
        [TestFixtureSetUp] // Runs once before any of the tests
        public void TestFixtureSetUp()
        {
        }

        [SetUp] // Runs before every tests
        public void SetUp()
        {
        }

        [TearDown] // Runs after every tests
        public void TearDown()
        {
        }

        [TestFixtureTearDown] // runs once after every tests are completed.
        public void TestFixtureTearDown()
        {
        }

        /// <summary>
        /// Create a dictionary to store test factory function
        /// </summary>
        /// <returns></returns>
        private IDictionary<Type, Func<DbContext, object>> GetTestFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                     {typeof(ITestRepository), DbContext => new TestRepository()} 
                };
        }

        [Test]
        public void GetRepositoryFactoryReturnsRepositoryFunctionIfTypeIsContainedInDictionary()
        {
            IDictionary<Type, Func<DbContext, object>> _repositoryFactories = GetTestFactories();
            RepositoryFactories repoFactory = new RepositoryFactories(_repositoryFactories);
            var aRepositoryFunction = repoFactory.GetRepositoryFactory<ITestRepository>();
            Assert.NotNull(aRepositoryFunction, "Should not return null if type is in the dictionary.");
        }

        [Test]
        public void GetRepositoryFactoryReturnsNullIfTypeIsNotContainedInDictionary()
        {
            IDictionary<Type, Func<DbContext, object>> _repositoryFactories = GetTestFactories();
            RepositoryFactories repoFactory = new RepositoryFactories(_repositoryFactories);
            var aRepositoryFunction = repoFactory.GetRepositoryFactory<ITestUnusedRepository>();
            Assert.Null(aRepositoryFunction, "Should return null if type is not in the dictionary.");
        }

        [Test]
        public void GetRepositoryFactoryForEntityTypeReturnsRepositoryFunctionIfTypeIsContainedInDictionary()
        {
            IDictionary<Type, Func<DbContext, object>> _repositoryFactories = GetTestFactories();
            RepositoryFactories repoFactory = new RepositoryFactories(_repositoryFactories);
            var aRepositoryFunction = repoFactory.GetDefaultOrExistingRepositoryFactoryForEntityType<ITestRepository>();
            Assert.That(aRepositoryFunction, Is.Not.Null, "a func should be returned");
            Assert.That(aRepositoryFunction, Is.TypeOf<System.Func<System.Data.Entity.DbContext, object>>(), "a func should be returned");
            var repository = aRepositoryFunction.Invoke(new DbContext("Useless connection string")); // this creates the repository
            Assert.That(repository, Is.TypeOf<TestRepository>(), "A TestRepository should be created");
        }

        [Test]
        public void GetDefaultOrExistingRepositoryFactoryForEntityTypeReturnsADefaultRepositoryFunctionIfTypeIsNotContainedInDictionary()
        {
            IDictionary<Type, Func<DbContext, object>> _repositoryFactories = GetTestFactories();
            RepositoryFactories repoFactory = new RepositoryFactories(_repositoryFactories);
            var aRepositoryFunction = repoFactory.GetDefaultOrExistingRepositoryFactoryForEntityType<ITestUnusedRepository>();
            Assert.That(aRepositoryFunction, Is.Not.Null, "a func should not be returned");
            Assert.That(aRepositoryFunction, Is.TypeOf<System.Func<System.Data.Entity.DbContext, object>>(), "a func should not be returned");
            var repository = aRepositoryFunction.Invoke(new DbContext("Useless connection string")); // this creates the repository
            Assert.That(repository, Is.TypeOf<SimpleTaskData.Repositories.DefaultRepository<ITestUnusedRepository>>(), "A DefaultRepository should be created");
        }
    }
}
