using Aju.Carefree.NetCore.Attributes;
using Aju.Carefree.NetCore.Cache;
using Microsoft.AspNetCore.Mvc;

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
            var str = DistributedCacheManager.Get("Aju");
            return str.Replace("\"", "");
        }

        private string _GetStr()
        {
            return "13276";
        }
    }
}