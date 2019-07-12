using Aju.Carefree.Common.DataBaseCore;
using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.Repositories.SqlSugar;
using Microsoft.Extensions.Options;
using System;
namespace Aju.Carefree.Repositories
{
    public class ItemDetailsRepository : GenericSqlSugarRepositoryBase<ItemsDetailEntity, string>, IItemDetailsRepository
    {
        public ItemDetailsRepository(IOptionsSnapshot<DbOption> options)
        {
            _dbOption = options.Get("Aju.Carefree");
            if (_dbOption == null)
                throw new ArgumentNullException(nameof(DbOption));
        }
    }
}
