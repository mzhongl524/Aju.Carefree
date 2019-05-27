using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    public class DemoController : Controller
    {
        private readonly IAreaService _areaService;
        public DemoController(IAreaService areaService) => _areaService = areaService;

        public IActionResult Index()
        {
            var model = _areaService.FindToPK("110000000000");
            return Content(model?.Name);
        }
    }
}