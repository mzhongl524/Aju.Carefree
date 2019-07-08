using Aju.Carefree.Common;
using Aju.Carefree.Common.ImageVerificationHelper;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.NetCore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Aju.Carefree.Web.Controllers
{
    public class AccountController : AjuCarfreControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public string Login(LoginViewModel viewModel)
        {
            BaseResult result = new BaseResult();
            //得到图片验证码
            var sessionValue = HttpContext.Session.GetString(_CaptchaCodeSessionName);
            if (sessionValue == viewModel.code)
            {
                //模拟登录加入缓存
                var admin = AddAdminCache();
                HttpContext.Session.Set(_OperatorCacheKey, ByteConvertHelper.Object2Bytes(admin));
                HttpContext.Session.Remove(_CaptchaCodeSessionName);
                return JsonHelper.Instance.Serialize(result);
            }
            result.ResultCode = ResultCodeAddMsgKeys.SignInCaptchaCodeErrorCode;
            result.ResultMsg = ResultCodeAddMsgKeys.SignInCaptchaCodeErrorMsg;
            return JsonHelper.Instance.Serialize(result);
        }

        public OperatorModel AddAdminCache()
        {
            return new OperatorModel
            {
                IsSystem = true,
                CompanyId = "Aju.Com",
                DepartmentId = "CTO",
                LoginIPAddress = "127.0.0.1",
                LoginIPAddressName = "127.0.0.1",
                LoginTime = DateTime.Now,
                LoginToken = "Aju",
                OrgId = "Aju",
                RoleId = "SuperAdmin",
                UserCode = "AjuAdmin",
                UserId = "Admin",
                UserName = "Aju",
                UserPwd = "Aju@123"
            };
        }

        public IActionResult GetCaptchaImage()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            HttpContext.Session.SetString(_CaptchaCodeSessionName, captchaCode);

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