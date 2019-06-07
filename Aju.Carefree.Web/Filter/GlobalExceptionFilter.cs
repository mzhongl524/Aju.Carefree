using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System.Net;

namespace Aju.Carefree.Web.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public void OnException(ExceptionContext filterContext)
        {
            logger.Error(filterContext.Exception);
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.ExceptionHandled = true;
        }
    }
}
