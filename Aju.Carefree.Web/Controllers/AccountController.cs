using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aju.Carefree.Common;
using Aju.Carefree.Dto.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public string Login()
        {
            BaseResult result = new BaseResult();
            return JsonHelper.Instance.Serialize(result);
        }
    }
}