using HouseRenting.Core.Contracts;
using HouseRenting.Web.Areas.Admin.Models;
using HouseRenting.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Areas.Admin.Controllers
{
    public class HousesController : AdminController
    {
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;

        public HousesController(IAgentService _agentService, IHouseService _houseService)
        {
            agentService = _agentService;
            houseService = _houseService;
        }
        [HttpGet]
        public IActionResult Mine()
        {
            var myHouses = new MyHousesViewModel();
            myHouses.RentedHouses = houseService.AllHousesByUserId(User.Id());

            myHouses.AddedHouses = houseService.AllHousesByAgentId(agentService.GetAgentId(User.Id()));
            return View(myHouses);
        }
    }
}
