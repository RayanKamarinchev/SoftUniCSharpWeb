using System.ComponentModel.DataAnnotations;

namespace HouseRenting.Core.Models.Houses
{
    public class AllHousesQueryModel
    {
        public int HousesPerPage { get; set; } = 3;
        public string Category { get; set; }
        [Display(Name="Search Term")]
        public string SearchTerm { get; set; }

        public HouseSorting HouseSorting { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalHousesCount { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
    }
}
