
using HouseRenting.Core.Contracts;
using HouseRenting.Data;

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
    }
}
