using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Dto;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aju.Carefree.Web.Controllers
{
    public class DemoController : Controller
    {
        private readonly IAreaService _areaService;
        public DemoController(IAreaService areaService) => _areaService = areaService;

        //读取数据
        public async Task<IActionResult> DbIndex()
        {
            var model = await _areaService.FindToPK("110000000000");
            return Content(model?.Name);
        }

        //public async Task<IActionResult> DbIndex2()
        //{
        //    var model = await _areaService.List();
        //    string str = "";
        //    foreach (var item in model.Take(10))
        //    {
        //        str += item.Name + "|";
        //    }
        //    return Content(str);
        //}
        //测试 AutoMapper
        public IActionResult AutoMapperIndex()
        {
            var model = new Areas { Code = "1", ParentCode = "0", Level = 1, Name = "1", Remark = "1" };
            var mapper = AutoMapperExt.MapTo<AreasDto>(model);
            return Content(JsonHelper.Instance.Serialize(mapper));
        }
    }
}