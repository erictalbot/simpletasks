using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleTaskData.Repositories;
using SimpleTaskData.NUnit.TestUtilities;

namespace SimpleTaskData.NUnit.Repositories
{
    [TestFixture]
    public class DefaultRepositoryTests : SqlFeeder
    {
        [TestFixtureSetUp] // Runs once before any of the tests
        public void TestFixtureSetUp()
        {
            ClearDatabaseContent();
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
            ClearDatabaseContent();
        }

        [Test]
        public void test()
        {
            SimpleTaskTestsDbContext dbContext = new SimpleTaskTestsDbContext();
            DefaultRepository<Task> aRepo = new DefaultRepository<Task>(dbContext);
            aRepo.Add(new Task() { Description = "Description" });
            dbContext.SaveChanges();
        }



    }
}
