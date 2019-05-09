using SqlSugar;
using DbType = SqlSugar.DbType;

namespace Aju.Carefree.Common.DataBaseCore
{
    public abstract class DbFactory
    {
        public static SqlSugarClient DB => GetInstance();
        static SqlSugarClient GetInstance()
        {
            string connectionString = "Server=127.0.0.1;Database=DB_Area;Integrated Security=False;User ID=sa;Password=123456;";
            var db = new SqlSugarClient(
                new ConnectionConfig
                {
                    ConnectionString = connectionString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                }
            );
            return db;
        }
    }

    //public static class DbFactory
    //{
    //    //  private IConfiguration _configuration { get; set; }
    //    public static void AddConfiguration()
    //    {

    //    }
    //    //private static IConfiguration _configuration;
    //    //public DbFactory(IConfiguration configuration)
    //    //{
    //    //    _configuration = configuration;
    //    //}
    //    ////SqlSugar
    //    public static SqlSugarClient GetSqlSugarClient() => new SqlSugarClient(new ConnectionConfig()
    //    {
    //        ConnectionString = configuration.GetConnectionString("DefaultConnection"),
    //        DbType = DbType.SqlServer,
    //        IsAutoCloseConnection = true,
    //        InitKeyType = InitKeyType.Attribute
    //    });
    //}
}