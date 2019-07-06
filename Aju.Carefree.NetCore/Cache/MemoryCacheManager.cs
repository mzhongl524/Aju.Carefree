using Aju.Carefree.NetCore.IOC;
using Microsoft.Extensions.Caching.Memory;

namespace Aju.Carefree.NetCore.Cache
{
    public class MemoryCacheManager
    {
        public static IMemoryCache GetInstance() => AspectCoreContainer.Resolve<IMemoryCache>();
    }
}
