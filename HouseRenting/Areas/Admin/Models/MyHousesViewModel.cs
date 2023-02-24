using HouseRenting.Core.Models.Houses;

namespace HouseRenting.Web.Areas.Admin.Models
{
    public class MyHousesViewModel
    {
        public IEnumerable<HouseServiceModel> AddedHouses { get; set; }
        public IEnumerable<HouseServiceModel> RentedHouses { get; set; }
    }
}
