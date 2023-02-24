using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models.Agents;
using HouseRenting.Core.Models.Houses;
using HouseRenting.Data;
using HouseRenting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext context;

        public HouseService(HouseRentingDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<HouseIndexServiceModel> GetLastThree()
        {
            return context.Houses.OrderByDescending(h => h.Id)
                          .Select(c => new HouseIndexServiceModel()
                          {
                              Id = c.Id,
                              Address = c.Address,
                              ImageUrl = c.ImageUrl,
                              Title = c.Title
                          })
                          .Take(3); ;
        }

        public IEnumerable<HouseCategoryServiceModel> AllCategories()
        {
            return context.Categories.Select(c => new HouseCategoryServiceModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public bool CategoryExists(int categoryId)
        {
            return context.Categories.Any(c => c.Id == categoryId);
        }

        public int Create(string title, string address, string description, string imageUrl, decimal price, int categoryId,
                          int agentId)
        {
            House house = new House()
            {
                Address = address,
                AgentId = agentId,
                CategoryId = categoryId,
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price
            };
            context.Houses.Add(house);
            context.SaveChanges();
            return house.Id;
        }

        public HouseQueryServiceModel All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newset,
                                          int currentPage = 1, int housesPerPage = 1)
        {
            var houseQuery = context.Houses.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                houseQuery =context.Houses.Where(h => h.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                houseQuery = houseQuery.Where(h=>h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                                                 h.Description.ToLower().Contains(searchTerm.ToLower()) ||
                                                 h.Address.ToLower().Contains(searchTerm.ToLower()));
            }

            houseQuery = sorting switch
            {
                HouseSorting.Price => houseQuery.OrderBy(h => h.PricePerMonth),
                HouseSorting.Newset => houseQuery.OrderByDescending(h => h.Id),
                HouseSorting.NotRentedFirst => houseQuery.OrderBy(h => h.RenterId != null).ThenByDescending(h => h.Id)
            };

            var houses = houseQuery.Skip(housesPerPage * (currentPage - 1))
                                   .Take(housesPerPage)
                                   .Select(h => new HouseServiceModel()
                                   {
                                       Address = h.Address,
                                       Id = h.Id,
                                       ImageUrl = h.ImageUrl,
                                       IsRented = h.RenterId != null,
                                       PricePerMonth = h.PricePerMonth
                                   });
            return new HouseQueryServiceModel()
            {
                Houses = houses,
                TotalHousesCount = houses.Count()
            };
        }

        public IEnumerable<string> AllCategoryNames()
        {
            return context.Categories.Select(c => c.Name).Distinct().ToList();
        }

        public IEnumerable<HouseServiceModel> AllHousesByUserId(string userId)
        {
            return context.Houses.Where(h => h.RenterId == userId)
                          .Select(h=> new HouseServiceModel()
                          {
                              Address = h.Address,
                              Id=h.Id,
                              ImageUrl = h.ImageUrl,
                              IsRented = true,
                              PricePerMonth = h.PricePerMonth,
                              Title = h.Title
                          }).ToList();
        }

        public IEnumerable<HouseServiceModel> AllHousesByAgentId(int agentId)
        {
            return context.Houses.Where(h=>h.AgentId == agentId)
                          .Select(h => new HouseServiceModel()
                          {
                              Address = h.Address,
                              Id = h.Id,
                              ImageUrl = h.ImageUrl,
                              IsRented = h.RenterId != null,
                              PricePerMonth = h.PricePerMonth,
                              Title = h.Title
                          }).ToList();
        }

        public bool Exists(int id)
        {
            return context.Houses.Any(h => h.Id == id);
        }

        public HouseDetailsServiceModel HouseDetailsById(int id)
        {
            return context.Houses.Where(h => h.Id == id)
                          .Select(h => new HouseDetailsServiceModel()
                          {
                              Id = h.Id,
                              Title = h.Title,
                              Address = h.Address,
                              Category = h.Category.Name,
                              Description = h.Description,
                              IsRented = h.RenterId != null,
                              ImageUrl = h.ImageUrl,
                              PricePerMonth = h.PricePerMonth,
                              Agent = new AgentServiceModel()
                              {
                                  PhoneNumber = h.Agent.PhoneNumber,
                                  Email = h.Agent.User.Email
                              }
                          }).FirstOrDefault();
        }

        public void Edit(int houseId, string title, string address, string description, string imageUrl, decimal price,
                         int categoryId)
        {
            var house = context.Houses.Find(houseId);
            house.Title = title;
            house.Address = address;
            house.Description = description;
            house.ImageUrl = imageUrl;
            house.PricePerMonth = price;
            house.CategoryId = categoryId;
            context.SaveChanges();
        }

        public int GetHouseCategoryId(int houseId)
        {
            return context.Houses.Find(houseId).CategoryId;
        }

        public bool HasAgentWithId(int houseID, string userId)
        {
            var house = context.Houses.Include(h=>h.Agent).FirstOrDefault(h => h.Id == houseID);
            if (house == null)
            {
                return false;
            }

            if (house.Agent.UserId != userId)
            {
                return false;
            }

            return true;
        }

        public void Delete(int houseId)
        {
            var house = context.Houses.FirstOrDefault(h => h.Id == houseId);
            context.Houses.Remove(house);
            context.SaveChanges();
        }

        public bool IsRented(int id)
        {
            var house = context.Houses.Find(id);
            return house.RenterId != null;
        }

        public void Rent(int id, string userId)
        {
            var house = context.Houses.Find(id);
            house.RenterId = userId;
            context.SaveChanges();
        }

        public bool IsRentedByUserId(int houseId, string userId)
        {
            var house = context.Houses.Find(houseId);
            if (house == null)
            {
                return false;
            }

            return house.RenterId != userId;
        }

        public void Leave(int houseId)
        {
            var house = context.Houses.Find(houseId);
            house.RenterId = null;
            context.SaveChanges();
        }
    }
}
