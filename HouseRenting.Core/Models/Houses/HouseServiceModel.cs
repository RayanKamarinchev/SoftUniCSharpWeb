using System.ComponentModel.DataAnnotations;

namespace HouseRenting.Core.Models.Houses
{
    public class HouseServiceModel : IHouseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
        [Display(Name = "Price per month")]
        public decimal PricePerMonth { get; set; }
        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}
