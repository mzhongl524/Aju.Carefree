using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Aju.Carefree.WebManager.Controllers
{
    public class NLogTestController : Controller
    {
        /*
         * 使用 Microsoft.Extensions.Logging  记录日志
         */
        private ILogger<NLogTestController> _logger;
        public NLogTestController(ILogger<NLogTestController> logger) => _logger = logger;
        /*
         * NLog 日志记录
         */
        //private static Logger nlog = LogManager.GetLogger("Aju");

        public IActionResult Index()
        {
            _logger.LogError("NLog 日志测试");

            //nlog.Info("普通信息日志-----------");
            //nlog.Debug("调试日志-----------");
            //nlog.Error("错误日志-----------");
            //nlog.Fatal("异常日志-----------");
            //nlog.Warn("警告日志-----------");
            //nlog.Trace("跟踪日志-----------");
            //nlog.Log(NLog.LogLevel.Warn, "Log日志------------------");
            return Content("日志记录");
        }
    }
}