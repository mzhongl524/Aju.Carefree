using Aju.Carefree.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aju.Carefree.IServices
{
    public interface IAreaService : IService<Areas, string>
    {
        Task<Areas> FindToPK(string id);

        Task<IEnumerable<Areas>> List();
    }
}
