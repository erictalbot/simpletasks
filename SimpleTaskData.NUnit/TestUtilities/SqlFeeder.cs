using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SimpleTaskData.NUnit.TestUtilities
{
    public class SqlFeeder
    {
        #region Constants

        private const string _sqlConnString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CanamVault-UnitTest;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework;";

        #endregion

        protected void ClearDatabaseContent()
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(Resources.ClearDatabaseContent, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        protected void FillDatabaseContent(string fillDatabaseScript)
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(fillDatabaseScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

    }
}
