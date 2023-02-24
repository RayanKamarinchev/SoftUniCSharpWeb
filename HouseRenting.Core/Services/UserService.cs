
using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models;
using HouseRenting.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.Services
{
    public class UserService : IUserService
    {
        private readonly HouseRentingDbContext context;

        public UserService(HouseRentingDbContext _context)
        {
            context = _context;
        }
        public string UserFullName(string userId)
        {
            var user = context.Users.Find(userId);
            if (string.IsNullOrEmpty(user.FistName) || string.IsNullOrEmpty(user.LastName))
            {
                return null;
            }
            return user.FistName + " " + user.LastName;
        }

        public IEnumerable<UserServiceModel> All()
        {
            var allUsers = new List<UserServiceModel>();
            var agents = context.Agents
                                .Include(a => a.User)
                                .Select(a => new UserServiceModel()
                                {
                                    Email = a.User.Email,
                                    FullName = a.User.FistName + " " + a.User.LastName,
                                    PhoneNumber = a.PhoneNumber
                                }).ToList();
            var users = context.Users
                               .Select(u => new UserServiceModel()
                               {
                                   Email = u.Email,
                                   FullName = u.FistName + " " + u.LastName,
                                   PhoneNumber = u.PhoneNumber
                               }).ToList();
            allUsers.AddRange(agents);
            allUsers.AddRange(users);
            return allUsers;
        }
    }
}
