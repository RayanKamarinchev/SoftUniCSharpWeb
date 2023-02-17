using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models
{
    public class PostViewModel
    {
        [UIHint("hidden")]
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please specify the title")]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; }
        [Display(Name = "Content")]
        [Required]
        [StringLength(1500, MinimumLength = 30)]
        public string Content { get; set; }
    }
}
