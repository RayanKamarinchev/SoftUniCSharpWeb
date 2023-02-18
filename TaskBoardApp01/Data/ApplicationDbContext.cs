using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp01.Data.Entities;
using Task = TaskBoardApp01.Data.Entities.Task;

namespace TaskBoardApp01.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        private User GuestUser { get; set; }
        public Board OpenBoard { get; set; }
        public Board InProgressBoard { get; set; }
        public Board DoneBoard { get; set; }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<User>();
            GuestUser = new User()
            {
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@gmail.com",
                NormalizedEmail = "GUEST@GMAIL.COM",
                FirstName = "Guest",
                LastName = "User",
                PasswordHash = hasher.HashPassword(GuestUser, "guest")
            };
        }

        private void SeedBoards()
        {
            OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };
            InProgressBoard = new Board()
            {
                Id = 2,
                Name = "InProgress"
            };
            DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }

        private List<Task> GetTasks()
        {
            return new List<Task>()
            {
                new Task()
                {
                    Id = 1,
                    Title = "Prepare for ASP.NET Fundamentals exam",
                    Description = "Learn using ASP.NET Core Idenity",
                    CreatedOn = DateTime.Now.AddMonths(-1),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.OpenBoard.Id
                },
                new Task()
                {
                    Id = 2,
                    Title = "Improve EF Core skills",
                    Description = "Learn using EF Core and MS SQL Server Management Studio",
                    CreatedOn = DateTime.Now.AddMonths(-5),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id
                },
                new Task()
                {
                    Id = 3,
                    Title = "Improve ASP.NET Core skills",
                    Description = "Learn using ASP.NET Core Identity",
                    CreatedOn = DateTime.Now.AddDays(-10),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.InProgressBoard.Id
                },
                new Task()
                {
                    Id = 4,
                    Title = "Prepare for C# Fundamentals Exam",
                    Description = "Prepare by solving old Mid and Final exams",
                    CreatedOn = DateTime.Now.AddYears(-1),
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id
                }
            };
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Task>()
                   .HasOne(t => t.Board)
                   .WithMany(t => t.Tasks)
                   .HasForeignKey(t => t.BoardId)
                   .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder.Entity<User>().HasData(GuestUser);
            SeedBoards();
            builder.Entity<Board>().HasData(OpenBoard, InProgressBoard, DoneBoard);
            builder.Entity<Task>().HasData(GetTasks());

            base.OnModelCreating(builder);

            
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }
    }
}