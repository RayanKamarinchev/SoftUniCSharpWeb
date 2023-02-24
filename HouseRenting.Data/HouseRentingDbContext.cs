using HouseRenting.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Data
{
    public class HouseRentingDbContext : IdentityDbContext<User>
    {
        public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<House>()
                   .HasOne(h => h.Category)
                   .WithMany(h => h.Houses)
                   .HasForeignKey(h => h.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<House>()
                   .HasOne(h => h.Agent)
                   .WithMany()
                   .HasForeignKey(h => h.AgentId)
                   .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder.Entity<User>()
                   .HasData(AgentUser, GuestUser, AdminUser);

            SeedAgent();
            builder.Entity<Agent>()
                   .HasData(Agent, AdminAgent);

            SeedCategories();
            builder.Entity<Category>()
                   .HasData(CottageCategory, SingleCategory, DuplexCategory);
            
            SeedHouses();
            builder.Entity<House>()
                   .HasData(FirstHouse, SecondHouse, ThirdHouse);
            base.OnModelCreating(builder);
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Agent> Agents { get; set; }

        //Seed
        public User AgentUser { get; set; }
        public User GuestUser { get; set; }
        public Agent Agent { get; set; }
        public Category CottageCategory { get; set; }
        public Category SingleCategory { get; set; }
        public Category DuplexCategory { get; set; }
        public House FirstHouse { get; set; }
        public House SecondHouse { get; set; }
        public House ThirdHouse { get; set; }
        public User AdminUser { get; set; }
        public Agent AdminAgent { get; set; }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            this.AgentUser = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com",
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com",
                FistName = "Raicho",
                LastName = "Kamarinchev"
            };

            this.AgentUser.PasswordHash =
                 hasher.HashPassword(this.AgentUser, "agent123");

            this.GuestUser = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                FistName = "Todor",
                LastName = "Teodosiev"
            };

            this.GuestUser.PasswordHash =
            hasher.HashPassword(this.AgentUser, "guest123");

            this.AdminUser = new User()
            {
                Id = "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
                UserName = "admin@mail.com",
                NormalizedUserName = "admin@mail.com",
                FistName = "Great",
                LastName = "Admin"
            };

            this.AdminUser.PasswordHash =
                hasher.HashPassword(this.AgentUser, "admin123");
        }

        private void SeedAgent()
        {
            this.Agent = new Agent()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                UserId = this.AgentUser.Id
            };
            this.AdminAgent = new Agent()
            {
                Id = 5,
                PhoneNumber = "+359123456789",
                UserId = this.AdminUser.Id
            };
        }

        private void SeedCategories()
        {
            this.CottageCategory = new Category()
            {
                Id = 1,
                Name = "Cottage"
            };

            this.SingleCategory = new Category()
            {
                Id = 2,
                Name = "Single-Family"
            };

            this.DuplexCategory = new Category()
            {
                Id = 3,
                Name = "Duplex"
            };
        }

        private void SeedHouses()
        {
            this.FirstHouse = new House()
            {
                Id = 1,
                Title = "Big House Marina",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
                PricePerMonth = 2100.00M,
                CategoryId = this.DuplexCategory.Id,
                AgentId = this.Agent.Id,
                RenterId = this.GuestUser.Id
            };

            this.SecondHouse = new House()
            {
                Id = 2,
                Title = "Family House Comfort",
                Address = "Near the Sea Garden in Burgas, Bulgaria",
                Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                PricePerMonth = 1200.00M,
                CategoryId = this.SingleCategory.Id,
                AgentId = this.Agent.Id
            };

            this.ThirdHouse = new House()
            {
                Id = 3,
                Title = "Grand House",
                Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                Description = "This luxurious house is everything you will need. It is just excellent.",
                ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                PricePerMonth = 2000.00M,
                CategoryId = this.SingleCategory.Id,
                AgentId = this.Agent.Id
            };
        }

    }
}