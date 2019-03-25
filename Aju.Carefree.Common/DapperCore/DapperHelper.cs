using System.Data;
using System.Data.SqlClient;

namespace Aju.Carefree.Common.DapperCore
{
    public class DapperHelper
    {
        public static string DapperDbConnectionString { get; set; }

        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DapperDbConnectionString;
            }
            IDbConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
