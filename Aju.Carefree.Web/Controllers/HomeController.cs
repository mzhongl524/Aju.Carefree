using Aju.Carefree.NetCore.Attributes;
using Aju.Carefree.NetCore.Cache;
using Aju.Carefree.NetCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aju.Carefree.Web.Controllers
{
    [TypeFilter(typeof(HandleLoginAsyncAttribute))]
    public class HomeController : AjuCarfreControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        [RedisCache(CacheKey = "Aju")]
        public virtual string RedisIndex()
        {
            var s1 = "1213243214";
            var s = _GetStr();
            return s1;
        }

        public string GetRedisValue()
        {
            string str = "xzxzxzxxzx";
            DistributedCacheManager.Set("XXX", ByteConvertHelper.Object2Bytes(str), options: new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
            var val = DistributedCacheManager.GetByte("XXX");
            return (string)ByteConvertHelper.Bytes2Object(val);
        }

        private string _GetStr()
        {
            return "13276";
        }
    }
}