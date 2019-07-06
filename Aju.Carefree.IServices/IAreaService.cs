using Aju.Carefree.Entity;
using Aju.Carefree.NetCore.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aju.Carefree.IServices
{
    public interface IAreaService : IService<Areas, string>
    {
        [RedisCache(CacheKey = "Area_ID_1")]
        Task<Areas> FindToPK(string id);

        Task<IEnumerable<Areas>> List();
    }
}
