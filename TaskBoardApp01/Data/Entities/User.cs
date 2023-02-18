using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskBoardApp01.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }
    }
}
