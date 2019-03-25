using System.Data;
using Aju.Carefree.Common.DapperCore;
using SqlSugar;
using DbType = SqlSugar.DbType;

namespace Aju.Carefree.Common.DataBaseCore
{
    public class DbFactory
    {
        public static string DbConnectionString { get; set; }
        //SqlSugar
        public static SqlSugarClient GetSqlSugarClient => new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = DbConnectionString,
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });
    }
}