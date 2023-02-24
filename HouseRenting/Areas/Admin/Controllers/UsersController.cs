using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static HouseRenting.Data.AdminConstants;
namespace HouseRenting.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache cache;

        public UsersController(IUserService _userService, IMemoryCache _cache)
        {
            userService = _userService;
            cache = _cache;
        }
        [Route("Users/All")]
        public IActionResult All()
        {
            var users = cache.Get<IEnumerable<UserServiceModel>>(UsersCacheKey);
            if (users == null)
            {
                users = userService.All();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                cache.Set(UsersCacheKey, users, cacheOptions);
            }
            return View(users);
        }
    }
}
