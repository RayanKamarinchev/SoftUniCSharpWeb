using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Rents.Models;
using HouseRenting.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.Rents
{
    public class RentService : IRentService
    {
        private readonly HouseRentingDbContext context;
        public RentService(HouseRentingDbContext _cotnext)
        {
            context = _cotnext;
        }
        public IEnumerable<RentServiceModel> All()
        {
            return context.Houses
                          .Include(h => h.Agent.User)
                          .Include(h => h.Renter)
                          .Where(h => h.RenterId != null)
                          .Select(h => new RentServiceModel()
                          {
                              AgentEmail = h.Agent.User.Email,
                              AgentFullName = h.Agent.User.FistName + " " + h.Agent.User.LastName,
                              HouseImageUrl = h.ImageUrl,
                              HouseTitle = h.Title,
                              RenterEmail = h.Renter.Email,
                              RenterFullName = h.Renter.FistName + " " + h.Renter.LastName
                          }).ToList();
        }
    }
}
