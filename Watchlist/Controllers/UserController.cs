using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Data;

namespace Watchlist.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Register()
        //{
        //    if (User?.Identity?.IsAuthenticated ?? false)
        //    {
        //        return RedirectToAction("All", "Movies");
        //    }

        //    var model = RegisterViewModel();
        //}
    }
}
