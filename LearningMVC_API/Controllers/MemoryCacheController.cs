using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningMVC_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LearningMVC_API.Controllers
{
    public class MemoryCacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemoryCacheController(IMemoryCache memoryCache, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            DateTime currentTime;
            bool isExist = _memoryCache.TryGetValue("CacheTime", out currentTime);
            if (!isExist)
            {
                currentTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(120));

                _memoryCache.Set("CacheTime", currentTime, cacheEntryOptions);
            }
            return View(currentTime);
        }
    }
}