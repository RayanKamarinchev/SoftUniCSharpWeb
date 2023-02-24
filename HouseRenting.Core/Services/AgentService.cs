using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Contracts;
using HouseRenting.Data;
using HouseRenting.Data.Entities;

namespace HouseRenting.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext context;

        public AgentService(HouseRentingDbContext _context)
        {
            context = _context;
        }

        public bool ExistsById(string userId)
        {
            return context.Agents.Any(a => a.UserId == userId);
        }

        public bool PersonWithPhoneNumberExists(string phoneNumber)
        {
            return context.Agents.Any(a => a.PhoneNumber == phoneNumber);
        }

        public bool UserHasRents(string userId)
        {
            return context.Houses.Any(h => h.RenterId == userId);
        }

        public void Create(string userID, string phoneNumber)
        {
            var agent = new Agent()
            {
                PhoneNumber = phoneNumber,
                UserId = userID
            };

            context.Agents.Add(agent);
            context.SaveChanges();
        }

        public int GetAgentId(string userId)
        {
            return context.Agents.FirstOrDefault(a => a.UserId == userId).Id;
        }
    }
}
