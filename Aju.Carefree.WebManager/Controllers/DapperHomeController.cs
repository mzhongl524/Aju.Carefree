using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.WebManager.Controllers
{
    public class DapperHomeController : Controller
    {
        private readonly IAreaDapperService _areaService;

        public DapperHomeController(IAreaDapperService areaDapperService)
        {
            _areaService = areaDapperService;
        }
        //private readonly ICacheService _cacheService;

        public async Task<IActionResult> Index()
        {
            var model = await _areaService.QueryFirstOrDefaultAsync("select * from TB_Area where Code=@code", new { code = "110000000000" });
            return Content(model.Code);
        }
    }
}
