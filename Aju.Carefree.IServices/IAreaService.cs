using Aju.Carefree.Entity;
using System.Threading.Tasks;

namespace Aju.Carefree.IServices
{
    public interface IAreaService : IService<Areas, string>
    {
        Areas FindToPK(string id);
    }
}
