using System.Collections.Generic;
using Aju.Carefree.Common.DataBaseCore;
using Aju.Carefree.Models;

namespace Aju.Carefree.GenerateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            //数据库连接字符串
            DbFactory.DbConnectionString = "Server=127.0.0.1;Database=DB_Area;Integrated Security=False;User ID=sa;Password=123456;";
            using (var db = DbFactory.GetSqlSugarClient)
            {
                db.CodeFirst.InitTables(typeof(Areas));

                var list = new List<Areas>
                {
                    new Areas
                    {
                        Code="110000000000",
                        Name="北京市",
                        Level=1,
                        Remark="province",
                        ParentCode="0"
                    },
                    new Areas
                    {
                        Code="110100000000",
                        Name="市辖区",
                        Level=2,
                        Remark="city",
                        ParentCode="110000000000"
                    },
                    new Areas
                    {
                        Code="110101000000",
                        Name="东城区",
                        Level=3,
                        Remark="area",
                        ParentCode="110100000000"
                    },
                    new Areas
                    {
                        Code="110101001000",
                        Name="东华门街道办事处",
                        Level=4,
                        Remark="town",
                        ParentCode="110101000000"
                    }
                };

                db.Insertable(list.ToArray()).ExecuteCommand();
            }
        }

    }
}
