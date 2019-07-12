using Aju.Carefree.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aju.Carefree.IServices
{
    public interface IItemDetailsService : IService<ItemsDetailEntity, string>
    {
        Task<IEnumerable<ItemsDetailEntity>> GetListAsync();

        Task<IEnumerable<ItemsDetailEntity>> GetListAsync(string itemId = "", string keyword = "");

        List<ItemsDetailEntity> GetListToSql(string enCode);

        Task<bool> SubmitFormAsync(ItemsDetailEntity entity, string keyValue);

        Task<bool> DeleteFormAsync(string keyValue);

        Task<IEnumerable<ItemsDetailEntity>> FindListByClauseAsync(Expression<Func<ItemsDetailEntity, bool>> where = null);

        Task<IEnumerable<ItemsDetailEntity>> FindListByClauseAsync(string itemId);
    }
}
