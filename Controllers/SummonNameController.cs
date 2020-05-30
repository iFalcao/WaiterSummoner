using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using WaiterSummoner.Services;

namespace WaiterSummoner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SummonNameController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "dailySummonName";

        public SummonNameController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var currentCacheValue = _cache.Get<string>(CACHE_KEY);

            if (currentCacheValue == null)
            {
                currentCacheValue = SummonNameGenerator.GetRandomSummonName();
                _cache.Set(CACHE_KEY, currentCacheValue, TimeSpan.FromDays(1));
            }

            return Ok(currentCacheValue);
        }
    }
}
