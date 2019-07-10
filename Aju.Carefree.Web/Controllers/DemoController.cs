using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Dto;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
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


        public string GetTableData(int page = 1, int limit = 10, string key = "")
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


        #region TreeTable数据

        public IActionResult TreeTableIndex()
        {
            return View();
        }
        public string GetTreeTable(string key = "")
        {
            string json = "[{\"authorityId\":1,\"authorityName\":\"系统管理\",\"orderNumber\":1,\"menuUrl\":null,\"menuIcon\":\"layui-icon-set\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":null,\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":0,\"parentId\":-1},{\"authorityId\":2,\"authorityName\":\"用户管理\",\"orderNumber\":2,\"menuUrl\":\"system/user\",\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":null,\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":0,\"parentId\":1},{\"authorityId\":3,\"authorityName\":\"查询用户\",\"orderNumber\":3,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/07/21 13:54:16\",\"authority\":\"user:view\",\"checked\":0,\"updateTime\":\"2018/07/21 13:54:16\",\"isMenu\":1,\"parentId\":2},{\"authorityId\":4,\"authorityName\":\"添加用户\",\"orderNumber\":4,\"menuUrl\":null,\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"user:add\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":2},{\"authorityId\":5,\"authorityName\":\"修改用户\",\"orderNumber\":5,\"menuUrl\":null,\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"user:edit\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":2},{\"authorityId\":6,\"authorityName\":\"删除用户\",\"orderNumber\":6,\"menuUrl\":null,\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"user:delete\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":2},{\"authorityId\":7,\"authorityName\":\"角色管理\",\"orderNumber\":7,\"menuUrl\":\"system/role\",\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":null,\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":0,\"parentId\":1},{\"authorityId\":8,\"authorityName\":\"查询角色\",\"orderNumber\":8,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/07/21 13:54:59\",\"authority\":\"role:view\",\"checked\":0,\"updateTime\":\"2018/07/21 13:54:58\",\"isMenu\":1,\"parentId\":7},{\"authorityId\":9,\"authorityName\":\"添加角色\",\"orderNumber\":9,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"role:add\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":7},{\"authorityId\":10,\"authorityName\":\"修改角色\",\"orderNumber\":10,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"role:edit\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":7},{\"authorityId\":11,\"authorityName\":\"删除角色\",\"orderNumber\":11,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"role:delete\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":7},{\"authorityId\":12,\"authorityName\":\"角色权限管理\",\"orderNumber\":12,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"role:auth\",\"checked\":0,\"updateTime\":\"2018/07/13 15:27:18\",\"isMenu\":1,\"parentId\":7},{\"authorityId\":13,\"authorityName\":\"权限管理\",\"orderNumber\":13,\"menuUrl\":\"system/authorities\",\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":null,\"checked\":0,\"updateTime\":\"2018/07/13 15:45:13\",\"isMenu\":0,\"parentId\":1},{\"authorityId\":14,\"authorityName\":\"查询权限\",\"orderNumber\":14,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/07/21 13:55:57\",\"authority\":\"authorities:view\",\"checked\":0,\"updateTime\":\"2018/07/21 13:55:56\",\"isMenu\":1,\"parentId\":13},{\"authorityId\":15,\"authorityName\":\"添加权限\",\"orderNumber\":15,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"authorities:add\",\"checked\":0,\"updateTime\":\"2018/06/29 11:05:41\",\"isMenu\":1,\"parentId\":13},{\"authorityId\":16,\"authorityName\":\"修改权限\",\"orderNumber\":16,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/07/13 09:13:42\",\"authority\":\"authorities:edit\",\"checked\":0,\"updateTime\":\"2018/07/13 09:13:42\",\"isMenu\":1,\"parentId\":13},{\"authorityId\":17,\"authorityName\":\"删除权限\",\"orderNumber\":17,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/06/29 11:05:41\",\"authority\":\"authorities:delete\",\"checked\":0,\"updateTime\":\"2018/06/29 11:05:41\",\"isMenu\":1,\"parentId\":13},{\"authorityId\":18,\"authorityName\":\"登录日志\",\"orderNumber\":18,\"menuUrl\":\"system/loginRecord\",\"menuIcon\":null,\"createTime\":\"2018/06/29 11:05:41\",\"authority\":null,\"checked\":0,\"updateTime\":\"2018/06/29 11:05:41\",\"isMenu\":0,\"parentId\":1},{\"authorityId\":19,\"authorityName\":\"查询登录日志\",\"orderNumber\":19,\"menuUrl\":\"\",\"menuIcon\":\"\",\"createTime\":\"2018/07/21 13:56:43\",\"authority\":\"loginRecord:view\",\"checked\":0,\"updateTime\":\"2018/07/21 13:56:43\",\"isMenu\":1,\"parentId\":18}]";
            var list = new List<TreeTableViewModel>();
            if (string.IsNullOrEmpty(key))
                list = JsonHelper.Instance.Deserialize<List<TreeTableViewModel>>(json);
            else
            {
                list = JsonHelper.Instance.Deserialize<List<TreeTableViewModel>>(json);
                list = list.Where(s => s.authorityName.Contains(key)).ToList();
            }
            return JsonHelper.Instance.Serialize(new TableDataModel { msg = "", code = 0, count = list.Count(), data = list });
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

    public class TreeTableViewModel
    {
        public int authorityId { get; set; }

        public string authorityName { get; set; }

        public int orderNumber { get; set; }

        public string menuUrl { get; set; }

        public string menuIcon { get; set; }

        public DateTime createTime { get; set; }

        public string authority { get; set; }

        public bool @checked { get; set; }

        public DateTime updateTime { get; set; }

        public int isMenu { get; set; }

        public int parentId { get; set; }
    }
}