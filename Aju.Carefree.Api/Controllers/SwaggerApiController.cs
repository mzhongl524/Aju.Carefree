using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Api.Controllers
{
    /// <summary>
    /// Swagger 测试
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SwaggerApiController : ControllerBase
    {
        /// <summary>
        /// HttpGet 
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        [HttpGet(Name = "Get")]
        public string Get(string name)
        {
            return "Hello " + name;
        }
    }
}