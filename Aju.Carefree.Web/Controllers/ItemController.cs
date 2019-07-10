using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    /// <summary>
    /// 系统字典
    /// </summary>
   // [HandleLoginAsync]
    public class ItemController : AjuCarfreControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}