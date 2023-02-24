using HouseRenting.Core.Rents;
using HouseRenting.Core.Rents.Models;
using HouseRenting.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
    public class RentsController : AdminController
    {
        private readonly IRentService rentService;
        private readonly IMemoryCache cache;

        public RentsController(IRentService _rentService, IMemoryCache _cache)
        {
            rentService = _rentService;
            cache = _cache;
        }
        [Route("Rents/All")]
        public IActionResult All()
        {
            var rents = cache.Get<IEnumerable<RentServiceModel>>(AdminConstants.RentsCacheKey);
            if (rents == null)
            {
                rents = rentService.All();
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                cache.Set(AdminConstants.RentsCacheKey, rents, options);
            }
            
            return View(rents);
        }
    }
}
