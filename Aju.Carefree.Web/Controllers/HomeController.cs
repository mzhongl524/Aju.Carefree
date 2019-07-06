using Aju.Carefree.NetCore.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    public class HomeController : Controller
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


        private string _GetStr()
        {
            return "13276";
        }
    }
}