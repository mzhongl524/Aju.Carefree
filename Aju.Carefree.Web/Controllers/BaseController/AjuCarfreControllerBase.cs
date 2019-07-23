using Aju.Carefree.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Aju.Carefree.Web.Controllers
{
    public abstract class AjuCarfreControllerBase : Controller
    {
        protected string _OperatorCacheKey = "Aju_Prince_OperatorProvider_20190708";
        protected string _CaptchaCodeSessionName = "CaptchaCode";


        protected virtual IActionResult Success(string message)
        {
            return Content(JsonHelper.Instance.Serialize(new AjaxResult { state = ResultType.success.ToString(), message = message }));
        }
        protected virtual IActionResult Success(string message, object data)
        {
            return Content(JsonHelper.Instance.Serialize(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }));
        }
        protected virtual IActionResult Error(string message)
        {
            return Content(JsonHelper.Instance.Serialize(new AjaxResult { state = ResultType.error.ToString(), message = message }));
        }

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HandleLoginAsyncAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Session.TryGetValue("Aju_Prince_OperatorProvider_20190708", out var result);
            if (result == null)
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            await next.Invoke();
        }
    }


    public class AjaxResult
    {
        public object state { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
    public enum ResultType
    {
        info = 0,
        success = 1,
        warning = 2,
        error = 3
    }
}
