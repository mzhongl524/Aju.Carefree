using Aju.Carefree.Common;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Aju.Carefree.Web.Controllers
{
    /// <summary>
    /// 系统字典
    /// </summary>
    [HandleLoginAsync]
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
            Dto.ItemDto viewModel = new Dto.ItemDto();
            if (string.IsNullOrEmpty(id))
                return View(viewModel);
            var data = await _itemService.GetItemsByPKID(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NetCore.Attributes.AjaxRequestOnly]
        public async Task<IActionResult> SubmitForm([FromForm]Dto.ItemDto viewModel)
        {
            await _itemService.SubmitFormAsync(viewModel, viewModel.Id);
            return Success("提交成功!");
        }

        /// <summary>
        /// 获取Item数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetItemData()
        {
            //TreeSelect 数据
            var data = await _itemService.GetTreeSelectViewModel();
            //序列化
            return JsonHelper.Instance.Serialize(data);
        }
    }
}