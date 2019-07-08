using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.Repositories.SqlSugar;

namespace Aju.Carefree.Repositories
{
    public class ItemDetailsRepository : GenericSqlSugarRepositoryBase<ItemsDetailEntity, string>, IItemDetailsRepository
    {
    }
}
