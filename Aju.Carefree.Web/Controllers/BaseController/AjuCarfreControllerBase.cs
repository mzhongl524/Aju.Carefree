using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aju.Carefree.Web.Controllers
{
    public abstract class AjuCarfreControllerBase : Controller
    {
        protected string _OperatorCacheKey = "Aju_Prince_OperatorProvider_20190708";
        protected string _CaptchaCodeSessionName = "CaptchaCode";
    }

    public class HandleLoginAsyncAttribute : IAsyncActionFilter
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
    public class HandleLoginAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
