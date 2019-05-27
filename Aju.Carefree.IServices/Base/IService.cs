using Aju.Carefree.Common;

namespace Aju.Carefree.IServices
{
    public interface IService<T, TKey> : IDependency where T : class, new()
    {

    }
}
