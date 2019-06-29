using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Dto;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public IActionResult TableIndex()
        {
            return View();
        }

        public string GetData()
        {
            var list = new List<ViewModel>();
            for (int i = 1; i < 100; i++)
            {
                list.Add(new ViewModel
                {
                    id = i.ToString(),
                    city = "兰州",
                    score = "10",
                    sex = "男",
                    sign = "Prince",
                    classify = "100",
                    experience = "1233",
                    wealth = "1111",
                    username = "Aju"
                });
            }
            return JsonHelper.Instance.Serialize(new TableDataModel { count = 99, data = list });
        }
        //测试 AutoMapper
        public IActionResult AutoMapperIndex()
        {
            var model = new Areas { Code = "1", ParentCode = "0", Level = 1, Name = "1", Remark = "1" };
            var mapper = AutoMapperExt.MapTo<AreasDto>(model);
            return Content(JsonHelper.Instance.Serialize(mapper));
        }
    }

    public class ViewModel
    {
        public string id { get; set; }
        public string username { get; set; }

        public string sex { get; set; }
        public string city { get; set; }
        public string sign { get; set; }
        public string experience { get; set; }
        public string score { get; set; }
        public string classify { get; set; }
        public string wealth { get; set; }
    }
}