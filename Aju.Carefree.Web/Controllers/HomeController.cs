using Aju.Carefree.Cache;
using Aju.Carefree.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aju.Carefree.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheService _cacheService;

        public HomeController(ICacheService cacheService) => _cacheService = cacheService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}