using Aju.Carefree.Cache;
using Aju.Carefree.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aju.Carefree.WebManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly ICacheService _cacheService;
        private IMapper _mapper { get; set; }


        public HomeController(IAreaService areaService, IMapper mapper, ICacheService cacheService)
        {
            _areaService = areaService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            //  var model = await _areaService.FindByClauseAsync(s => s.Code.Equals("110000000000"));
            var model = _areaService.FindToPK("110000000000");
            return Content(model.Name);
        }

        ////AutoMapper
        //public async Task<IActionResult> Mapper(string code = "110000000000")
        //{
        //    var model = await _areaService.FindByClauseAsync(s => s.Code.Equals(code));
        //    var dto = _mapper.Map<Areas, AreasDto>(model);
        //    return Content(dto.Name);
        //}

        public async Task<IActionResult> CacheDemo()
        {
            await _cacheService.SetAsync("aju", "Prince");

            return Content(await _cacheService.GetAsync("aju"));
        }
    }
}