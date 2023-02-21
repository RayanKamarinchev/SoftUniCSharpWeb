using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 5)]
        [Required]  
        public string Password { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        public IEnumerable<Movie> UserMovies { get; set; } = new List<Movie>();
    }
}
