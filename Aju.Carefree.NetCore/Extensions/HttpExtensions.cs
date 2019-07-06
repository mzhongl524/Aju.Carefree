using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Aju.Carefree.NetCore.Extensions
{
    public static class HttpExtensions
    {
        /// <summary>
        /// 判断当前请求是否为Ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return request.Headers.ContainsKey("X-Requested-With") &&
                   request.Headers["X-Requested-With"].Equals("XMLHttpRequest");
        }

        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
