using HouseRenting.Core.Models;

namespace HouseRenting.Core.Contracts
{
    public interface IUserService
    {
        string UserFullName(string userId);
        IEnumerable<UserServiceModel> All();
    }
}
