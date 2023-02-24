using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Models.Houses;

namespace HouseRenting.Core.Contracts
{
    public interface IHouseService
    {
        IEnumerable<HouseIndexServiceModel> GetLastThree();
        IEnumerable<HouseCategoryServiceModel> AllCategories();
        bool CategoryExists(int categoryId);

        int Create(string title, string address, string description, string imageUrl, decimal price, int categoryId,
                   int agentId);

        HouseQueryServiceModel All(string category = null, string searchTerm = null,
                                   HouseSorting sorting = HouseSorting.Newset, int currentPage = 1,
                                   int housesPerPage = 1);

        IEnumerable<string> AllCategoryNames();
        IEnumerable<HouseServiceModel> AllHousesByUserId(string userId);
        IEnumerable<HouseServiceModel> AllHousesByAgentId(int agentId);
        bool Exists(int id);
        HouseDetailsServiceModel HouseDetailsById(int id);

        void Edit(int houseId, string title, string address,
                  string description, string imageUrl, decimal price, int categoryId);

        int GetHouseCategoryId(int houseId);
        bool HasAgentWithId(int houseID, string userId);
        void Delete(int houseId);
        bool IsRented(int id);
        void Rent(int id, string userId);
        bool IsRentedByUserId(int houseId, string userId);
        void Leave(int houseId);
    }
}
