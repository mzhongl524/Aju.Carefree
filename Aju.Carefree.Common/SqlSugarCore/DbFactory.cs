using SqlSugar;
using DbType = SqlSugar.DbType;

namespace Aju.Carefree.Common.DataBaseCore
{
    public abstract class DbFactory
    {
        public static SqlSugarClient DB(string connStr) => GetInstance(connStr);
        static SqlSugarClient GetInstance(string connStr)
        {
            //Server=127.0.0.1;Database=DB_Area;Integrated Security=False;User ID=sa;Password=123456;
            var db = new SqlSugarClient(
                new ConnectionConfig
                {
                    ConnectionString = connStr,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                }
            );
            return db;
        }
    }
}