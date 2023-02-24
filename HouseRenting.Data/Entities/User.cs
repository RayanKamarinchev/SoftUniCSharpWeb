using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HouseRenting.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string FistName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; }
    }
}
