using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Dto;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aju.Carefree.Web.Controllers
{
    public class DemoController : Controller
    {
        private readonly IAreaService _areaService;
        private List<ViewModel> _list = new List<ViewModel>();

        //private readonly ICacheService _cache;

        public DemoController(IAreaService areaService)
        {
            _areaService = areaService;
            //_cache = cache;
            for (int i = 1; i < 100; i++)
            {
                _list.Add(new ViewModel
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
        }

        //public async Task<IActionResult> Index()
        //{
        //    //await _cache.SetStringAsync("123", "321");
        //    //var s = await _cache.GetStringAsync("123");
        //    return Content(s);
        //}

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


        public string GetTableData(int page = 1, int limit = 10)
        {
            page = page - 1;
            var list = _list.Skip(page * limit).Take(limit);
            return JsonHelper.Instance.Serialize(new TableDataModel { count = _list.Count(), data = list });
        }

        #region Authtree数据
        public IActionResult TreeIndex()
        {
            return View();
        }

        public string GetTreeData()
        {
            var list = new List<AuthTreeViewModelList>();
            list.Add(new AuthTreeViewModelList
            {
                @checked = true,
                name = "用户管理",
                value = "xsgl",
                list = new List<AuthTreeViewModelList>
                {
                    new AuthTreeViewModelList{ @checked=true,name="用户组",value="xsgl-basic",
                        list =new List<AuthTreeViewModelList>{
                            new AuthTreeViewModelList{ name="本站用户",value="xsgl-basic-xsxm",@checked=true,
                                list =new List<AuthTreeViewModelList>{
                                    new AuthTreeViewModelList{name="用户列表",value="xsgl-basic-xsxm-readonly",@checked=true},
                                    new AuthTreeViewModelList{name="新增用户",value="xsgl-basic-xsxm-editable",@checked=false}
                                } }
                        } },
                    new AuthTreeViewModelList{ name="第三方用户",value="xsgl-basic-xsxm",@checked=true,
                        list =new List<AuthTreeViewModelList>{
                             new AuthTreeViewModelList{name="用户列表",value="xsgl-basic-xsxm-readonly",@checked=false}
                        }
                    }
                }
            });
            list.Add(new AuthTreeViewModelList
            {
                @checked = true,
                name = "用户组管理",
                value = "sbgl",
                list = new List<AuthTreeViewModelList>
                {
                     new AuthTreeViewModelList{ @checked=true,name="角色管理",value="sbgl-sbsjlb",
                         list =new List<AuthTreeViewModelList>{
                              new AuthTreeViewModelList{name="添加角色",value="sbgl-sbsjlb-dj",@checked=true},
                               new AuthTreeViewModelList{name="角色列表",value="sbgl-sbsjlb-yl",@checked=false}
                     } },
                     new AuthTreeViewModelList{ @checked=true,name="管理员管理",value="sbgl-sbsjlb",
                         list =new List<AuthTreeViewModelList>{
                              new AuthTreeViewModelList{name="添加管理员",value="sbgl-sbsjlb-dj",@checked=true},
                               new AuthTreeViewModelList{name="管理员列表",value="sbgl-sbsjlb-yl",@checked=false}
                     } }
                }
            });
            return JsonHelper.Instance.Serialize(new AuthTreeViewModel { data = new AuthTreeViewModelExt { trees = list } });
        }
        #endregion

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