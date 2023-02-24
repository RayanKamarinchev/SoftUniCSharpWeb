using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HouseRenting.Data.Entities
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 7)]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
