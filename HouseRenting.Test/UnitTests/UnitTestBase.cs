using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Data;
using HouseRenting.Data.Entities;
using HouseRenting.Test.Mocks;
using NUnit.Framework;

namespace HouseRenting.Test.UnitTests
{
    public class UnitTestBase
    {
        protected HouseRentingDbContext context;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            context = DbMock.Instance;
            SeedDatabase();
        }

        public User Renter { get; private set; }
        public Agent Agent { get; private set; }
        public House RentedHouse { get; private set; }

        private void SeedDatabase()
        {
            this.Renter = new User()
            {
                Id = "RenterUserId",
                Email = "rent@er.bg",
                FistName = "Renter",
                LastName = "User"
            };

            context.Users.Add(this.Renter);

            this.Agent = new Agent()
            {
                PhoneNumber = "+359111111111",
                User = new User()
                {
                    Id = "TestUserId",
                    Email = "test@test.bg",
                    FistName = "Test",
                    LastName = "Tester"
                }
            };
            context.Agents.Add(this.Agent);

            this.RentedHouse = new House()
            {
                Title = "First Test House",
                Address = "Test, 201 Test",
                Description = "This is a test description. This is a test description. This is a test description.",
                ImageUrl =
                    "https://www.bhg.com/thmb/0Fg0imFSA6HVZMS2DFWPvjbYDoQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
                Renter = this.Renter,
                Agent = this.Agent,
                Category = new Category() { Name = "Cottage" }
            };

            context.Houses.Add(this.RentedHouse);

            var nonRentedHouse = new House()
            {
                Title = "Second Test House",
                Address = "Test, 204 Test",
                Description = "This is another test description. This is another test description.",
                ImageUrl = "https://images.adsttc.com/media/images/629f/3517/c372/5201/650f/1c7f/large_jpg/hyde-park-house-robeson-architects_1.jpg?1654601149",
                Renter = this.Renter,
                Agent = this.Agent,
                Category = new Category() { Name = "Single-Family" }
            };

            context.Houses.Add(nonRentedHouse);
            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownDb()
        {
            context.Dispose();
        }
    }
}
