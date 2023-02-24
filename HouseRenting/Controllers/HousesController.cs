using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models.Houses;
using HouseRenting.Data;
using HouseRenting.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HouseRenting.Web.Controllers
{
    public class HousesController : Controller
    {
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;
        private readonly IMemoryCache cache;

        public HousesController(IAgentService _agentService, IHouseService _houseService, IMemoryCache _cache)
        {
            agentService = _agentService;
            houseService = _houseService;
            cache = _cache;
        }
        public IActionResult Index([FromQuery] AllHousesQueryModel query)
        {
            var queryRes = houseService.All(query.Category, query.SearchTerm, query.HouseSorting, query.CurrentPage,
                                            query.HousesPerPage);
            query.TotalHousesCount = queryRes.TotalHousesCount;
            query.Houses = queryRes.Houses;
            var houseCategories = houseService.AllCategoryNames();
            query.Categories = houseCategories;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            IEnumerable<HouseServiceModel> houses = null;
            string userId = User.Id();
            if (agentService.ExistsById(userId))
            {
                var agentId = agentService.GetAgentId(userId);
                houses = houseService.AllHousesByAgentId(agentId);
            }
            else
            {
                houses = houseService.AllHousesByUserId(userId);
            }

            return View(houses);
        }

        [Authorize]
        public IActionResult Details(int id, string information)
        {
            if (!houseService.Exists(id))
            {
                return BadRequest();
            }

            var house = houseService.HouseDetailsById(id);
            if (information != house.GetInformation())
            {
                return BadRequest();
            }

            return View(house);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!agentService.ExistsById(User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }
            return View(new HouseFormModel()
            {
                Categories = houseService.AllCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(HouseFormModel model)
        {
            if (!agentService.ExistsById(User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            if (!houseService.CategoryExists(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Invalid category Id");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = houseService.AllCategories();
                return View(model);
            }

            int agentId = agentService.GetAgentId(User.Id());
            var houseId = houseService.Create(model.Title, model.Address, model.Description, model.ImageUrl,
                                                 model.PricePerMonth, model.CategoryId, agentId);
            TempData["message"] = "You have successfully added a house!";
            return RedirectToAction(nameof(Details), new { id = houseId, information = model.GetInformation() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!houseService.Exists(id))
            {
                return BadRequest();
            }

            if (!houseService.HasAgentWithId(id, User.Id()) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var house = houseService.HouseDetailsById(id);
            var houseCategoryId = houseService.GetHouseCategoryId(house.Id);
            var houseModel = new HouseFormModel()
            {
                Address = house.Address,
                Title = house.Title,
                PricePerMonth = house.PricePerMonth,
                ImageUrl = house.ImageUrl,
                Description = house.Description,
                CategoryId = houseCategoryId,
                Categories = houseService.AllCategories()
            };
            TempData["message"] = "You have successfully edited a house!";
            return View(houseModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, HouseFormModel model)
        {
            if (!houseService.Exists(id))
            {
                return View();
            }

            if (!houseService.HasAgentWithId(id, User.Id()) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!houseService.CategoryExists(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = houseService.AllCategories();

                return View(model);
            }
            houseService.Edit(id, model.Title, model.Address, model.Description
                              , model.ImageUrl, model.PricePerMonth, model.CategoryId);
            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!houseService.Exists(id))
            {
                return BadRequest();
            }

            if (!this.houseService.HasAgentWithId(id, User.Id()) && !User.IsAdmin() && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var house = houseService.HouseDetailsById(id);
            var model = new HouseDetailsViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Id = house.Id
            };
            TempData["message"] = "You have successfully deleted a house!";
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(HouseDetailsViewModel model)
        {
            if (!houseService.Exists(model.Id))
            {
                return BadRequest();
            }

            if (!this.houseService.HasAgentWithId(model.Id, User.Id()) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            houseService.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Rent(int id)
        {
            if (!houseService.Exists(id))
            {
                return BadRequest();
            }

            if (!agentService.ExistsById(User.Id()) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            if (houseService.IsRented(id))
            {
                return BadRequest();
            }
            houseService.Rent(id, User.Id());
            cache.Remove(AdminConstants.RentsCacheKey);
            TempData["message"] = "You have successfully rented a house!";
            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Leave(int id)
        {
            if (!houseService.Exists(id))
            {
                return BadRequest();
            }

            if (!houseService.IsRentedByUserId(id, User.Id()))
            {
                return Unauthorized();
            }
            houseService.Leave(id);
            cache.Remove(AdminConstants.RentsCacheKey);
            TempData["message"] = "You have successfully left a house!";
            return RedirectToAction(nameof(Mine));
        }
    }
}
