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
            db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            {

            };
            db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
            {

            };
            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                //exp.sql exp.parameters 可以拿到参数和错误Sql             
            };
            //db.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
            //{
            //    // return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            //};
            return db;
        }
    }

    public class DbOption
    {
        public string ConnectionString { get; set; }

        public string DbType { get; set; }
    }
}