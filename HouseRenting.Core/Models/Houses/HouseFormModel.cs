using HouseRenting.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRenting.Core.Models.Houses
{
    public class HouseFormModel : IHouseModel
    {
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 30)]
        public string Address { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 50)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
        [Required]
        [Range(0.00, 2000, ErrorMessage = "Price must be positive and less than {2} leva")]
        public decimal PricePerMonth { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new List<HouseCategoryServiceModel>();
    }
}
