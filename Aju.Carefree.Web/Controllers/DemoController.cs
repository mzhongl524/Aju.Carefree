using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Dto;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    public class DemoController : Controller
    {
        private readonly IAreaService _areaService;
        public DemoController(IAreaService areaService) => _areaService = areaService;

        public IActionResult DbIndex()
        {
            var model = _areaService.FindToPK("110000000000");
            return Content(model?.Name);
        }

        public IActionResult AutoMapperIndex()
        {
            var model = new Areas { Code = "1", ParentCode = "0", Level = 1, Name = "1", Remark = "1" };
            var mapper = AutoMapperExt.MapTo<AreasDto>(model);
            return Content(JsonHelper.Instance.Serialize(mapper));
        }
    }
}