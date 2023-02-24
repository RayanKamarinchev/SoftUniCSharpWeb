using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HouseRenting.Data.AdminConstants;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
    }
}
