using HouseRenting.Core.Contracts;
using HouseRenting.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static HouseRenting.Data.AdminConstants;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
