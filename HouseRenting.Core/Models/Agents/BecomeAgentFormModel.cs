using System.ComponentModel.DataAnnotations;

namespace HouseRenting.Web.Models.Agents
{
    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(15, MinimumLength = 7)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
