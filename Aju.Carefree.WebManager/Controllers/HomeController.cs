using System.Threading.Tasks;
using Aju.Carefree.Cache;
using Aju.Carefree.Dto;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.WebManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAreaSqlSugarService _areaService;
        private readonly ICacheService _cacheService;
        private IMapper _mapper { get; set; }


        public HomeController(IAreaSqlSugarService areaService, IMapper mapper, ICacheService cacheService)
        {
            _areaService = areaService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _areaService.FindByClauseAsync(s => s.Code.Equals("110000000000"));
            return Content(model.Code);
        }

        //AutoMapper
        public async Task<IActionResult> Mapper(string code = "110000000000")
        {
            var model = await _areaService.FindByClauseAsync(s => s.Code.Equals(code));
            var dto = _mapper.Map<Areas, AreasDto>(model);
            return Content(dto.Name);
        }

        public async Task<IActionResult> CacheDemo()
        {
            await _cacheService.SetAsync("aju", "Prince");

            return Content(await _cacheService.GetAsync("aju"));
        }
    }
}