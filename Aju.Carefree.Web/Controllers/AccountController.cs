using Aju.Carefree.Common;
using Aju.Carefree.Common.ImageVerificationHelper;
using Aju.Carefree.Dto.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Aju.Carefree.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string CaptchaCodeSessionName = "CaptchaCode";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public string Login(LoginViewModel viewModel)
        {
            //
            BaseResult result = new BaseResult();
            return JsonHelper.Instance.Serialize(result);
        }

        public IActionResult GetCaptchaImage()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            HttpContext.Session.SetString(CaptchaCodeSessionName, captchaCode);
            return new FileStreamResult(new MemoryStream(result.CaptchaByteData), "image/png");
        }
    }

    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string code { get; set; }
    }
}