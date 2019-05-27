using Aju.Carefree.IRepositories;
using Aju.Carefree.Entity;

namespace Aju.Carefree.Repositories
{
    public class AreaRepository : GenericSqlSugarRepositoryBase<Areas, string>, IAreaRepository
    {
        public AreaRepository() : base("Server=127.0.0.1;Database=DB_Area;Integrated Security=False;User ID=sa;Password=123456;") { }
        public AreaRepository(string connStr) : base(connStr)
        {
        }
    }
}
