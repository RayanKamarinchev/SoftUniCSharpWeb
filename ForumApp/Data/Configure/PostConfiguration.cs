using ForumApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configure
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            List<Post> posts = GetPosts();

            builder.HasData(posts);
        }

        private List<Post> GetPosts()
        {
            return new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first post will be CRUd operations."
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "MVC is very interesting"
                },
                new Post()
                {
                    Id = 3,
                    Title = "My third post",
                    Content = "C# is much better than CSS"
                }
            };
        }
    }
}
