﻿using HouseRenting.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models.Houses;

namespace HouseRenting.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService _houseService)
        {
            houseService = _houseService;
        }
        public IActionResult Index()
        {
            var houses = houseService.GetLastThree();
            return View(houses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}