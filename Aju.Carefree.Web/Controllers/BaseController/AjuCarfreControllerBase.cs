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
}
