using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aju.Carefree.WebManager.Controllers
{
    public class CacheDemoController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        //private readonly ICacheService _cacheService;
        public CacheDemoController(IDistributedCache distributedCache)
        //, ICacheService cacheService)
        {
            _distributedCache = distributedCache;
            // _cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _distributedCache.GetAsync("name-key");
            if (value == null)
            {
                var obj = new Dictionary<string, string>
                {
                    ["FirstName"] = "Nick",
                    ["LastName"] = "Jack"
                };
                var str = JsonConvert.SerializeObject(obj);
                var encode = System.Text.Encoding.UTF8.GetBytes(str);

                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));

                await _distributedCache.SetAsync("name-key", encode, option);
                //  await _cacheService.SetAsync("name-key", str, 30);
                return View(obj);
            }
            else
            {
                var str = System.Text.Encoding.UTF8.GetString(value);
                var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                return View(obj);
            }
        }
    }
}