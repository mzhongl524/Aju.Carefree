using SqlSugar;

namespace Aju.Carefree.Common.DataBaseCore
{
    public class DbFactory
    {
        public static string DbConnectionString { get; set; }
        public static SqlSugarClient GetSqlSugarClient => new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = DbConnectionString,
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });
    }
}
