using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aju.Carefree.Common;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    /// <summary>
    /// 系统字典
    /// </summary>
   // [HandleLoginAsync]
    public class ItemController : AjuCarfreControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IItemDetailsService _itemDetailsService;

        public ItemController(IItemService itemService, IItemDetailsService itemDetailsService)
        {
            _itemService = itemService;
            _itemDetailsService = itemDetailsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> GetData()
        {
            var data = await _itemService.GetViewModel();

            return JsonHelper.Instance.Serialize(data);
        }

        public async Task<string> GetSubData(string id, int page, int limit, string key)
        {
            var data = await _itemDetailsService.FindListByClauseAsync(id, key);
            return JsonHelper.Instance.Serialize(new TableDataModel
            {
                count = data.Count(),
                data = data.ToList()
            });
        }

        public IActionResult AddOrEditItem(string id)
        {
            return View();
        }
    }
}