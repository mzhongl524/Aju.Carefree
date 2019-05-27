using Aju.Carefree.IRepositories;
using Aju.Carefree.Entity;

namespace Aju.Carefree.Repositories
{
    public class AreaSqlSugarRepository : GenericSqlSugarRepositoryBase<Areas, string>, IAreaSqlSugarRepository
    {
        public AreaSqlSugarRepository(string connStr) : base(connStr)
        {
        }
    }
}
