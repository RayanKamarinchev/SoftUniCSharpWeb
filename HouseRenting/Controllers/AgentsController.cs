using HouseRenting.Core.Contracts;
using HouseRenting.Web.Infrastructure;
using HouseRenting.Web.Models.Agents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Controllers
{
    public class AgentsController : Controller
    {
        private readonly IAgentService agentService;

        public AgentsController(IAgentService _agentService)
        {
            agentService = _agentService;
        }   
        [Authorize]
        public IActionResult Become()
        {
            if (agentService.ExistsById(User.Id()))
            {
                return BadRequest();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel model)
        {
            if (agentService.ExistsById(User.Id()))
            {
                return BadRequest();
            }

            if (agentService.PersonWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "This phone number already exists");
            }
            if (agentService.UserHasRents(User.Id()))
            {
                ModelState.AddModelError("Error", "You should have no rents to be agent");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            agentService.Create(User.Id(), model.PhoneNumber);
            TempData["message"] = "You have successfully became an agent";
            return RedirectToAction(nameof(HousesController.Index), "Houses");
        }
    }
}
