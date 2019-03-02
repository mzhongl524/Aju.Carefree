using System.Threading.Tasks;
using Aju.Carefree.Dto;
using Aju.Carefree.IServices;
using Aju.Carefree.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.WebManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAreaService _areaService;
        private IMapper _mapper { get; set; }

        public HomeController(IAreaService areaService, IMapper mapper)
        {
            _areaService = areaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _areaService.FindByClauseAsync(s => s.Code.Equals("110000000000"));
            return Content(model.Code);
        }

        public async Task<IActionResult> Get(string code = "110000000000")
        {
            var model = await _areaService.FindByClauseAsync(s => s.Code.Equals(code));
            var dto = _mapper.Map<Areas, AreasDto>(model);
            return Content(dto.Name);
        }
    }
}