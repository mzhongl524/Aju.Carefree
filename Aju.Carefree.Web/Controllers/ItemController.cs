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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<string> GetData()
        {
            var data = await _itemService.GetViewModel();
            return JsonHelper.Instance.Serialize(data);
        }
        [HttpGet]
        public async Task<string> GetSubData(string id, int page, int limit, string key)
        {
            var data = await _itemDetailsService.FindListByClauseAsync(id, key);
            return JsonHelper.Instance.Serialize(new TableDataModel
            {
                count = data.Count(),
                data = data.ToList()
            });
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEditItem(string id)
        {
            Dto.ItemViewModel viewModel = new Dto.ItemViewModel();
            if (string.IsNullOrEmpty(id))
                return View(viewModel);
            var data = await _itemService.GetItemsByPKID(id);
            viewModel.EnCode = data.EnCode;
            viewModel.FullName = data.FullName;
            viewModel.Id = data.Id;
            viewModel.IsEnabled = (bool)data.EnabledMark;
            viewModel.ParentId = data.ParentId;
            viewModel.Remark = data.Description;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NetCore.Attributes.AjaxRequestOnly]
        public IActionResult SubmitForm([FromForm]Dto.ItemViewModel viewModel)
        {

            return Success("提交成功!");
        }

        [HttpGet]
        public async Task<string> GetItemData()
        {
            var data = await _itemService.GetTreeSelectViewModel();
            return JsonHelper.Instance.Serialize(data);
        }
    }
}