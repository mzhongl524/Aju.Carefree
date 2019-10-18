using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aju.Carefree.Common;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.Entity;
using Aju.Carefree.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Aju.Carefree.Web.Controllers
{
    public class MenuController : AjuCarfreControllerBase
    {
        private readonly IMenuService _service;
        public MenuController(IMenuService service)
        {
            _service = service;
        }

        public async Task<string> GetData(string key = "")
        {
            IEnumerable<MenusEntity> model = null;
            if (string.IsNullOrEmpty(key))
                model = await _service.GetMenusAsync();
            else
                model = await _service.GetMenusAsync(key);
            return JsonHelper.Instance.Serialize(new TableDataModel { msg = "", code = 0, count = model.Count(), data = model });
        }
    }
}