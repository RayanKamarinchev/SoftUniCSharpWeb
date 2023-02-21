using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Director { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [Range(0,10)]
        public decimal Rating { get; set; }
        [Required]
        public int GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }
        public IEnumerable<User> UserMovies { get; set; } = new List<User>();
    }
}
