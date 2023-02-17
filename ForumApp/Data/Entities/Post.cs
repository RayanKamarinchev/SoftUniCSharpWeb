using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Entities
{
    [Comment("Published posts")]
    public class Post
    {
        [Comment("Post identifier")]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        [Comment("Post Title")]
        public string Title { get; set; }
        [Required]
        [StringLength(1500, MinimumLength = 30)]
        [Comment("Content")]
        public string Content { get; set; }

        [Comment("Marks if post is deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
