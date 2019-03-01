using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.WebManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAreaService _areaService;

        public HomeController(IAreaService areaService) => _areaService = areaService;
        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            var model = await _areaService.FindByClauseAsync(s => s.Code.Equals("110000000000"));
            return Content(model.Code);
        }
    }
}