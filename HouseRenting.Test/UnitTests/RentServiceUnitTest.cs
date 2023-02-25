using HouseRenting.Core.Contracts;
using HouseRenting.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Rents;

namespace HouseRenting.Test.UnitTests
{
    public class RentServiceUnitTest : UnitTestBase
    {
        private IRentService rentService;

        [OneTimeSetUp]
        public void SetUp()
        {
            rentService = new RentService(context);
        }

        [Test]
        public void All_Correct()
        {
            var result = rentService.All();
            Assert.IsNotNull(result);

            var rentedHousesDb = context.Houses.Where(h => h.RenterId != null);
            Assert.AreEqual(result.Count(), rentedHousesDb.ToList().Count);

            var resultHouse = result.ToList()
                                    .Find(h => h.HouseTitle == RentedHouse.Title);
            Assert.IsNotNull(resultHouse);
            Assert.AreEqual(resultHouse.HouseTitle, RentedHouse.Title);
            Assert.AreEqual(resultHouse.RenterEmail, Renter.Email);
            Assert.AreEqual(resultHouse.AgentEmail, Agent.User.Email);
            Assert.AreEqual(resultHouse.RenterFullName, Renter.FistName + " " + Renter.LastName);
            Assert.AreEqual(resultHouse.AgentFullName, Agent.User.FistName + " " + Agent.User.LastName);
            Assert.AreEqual(resultHouse.HouseImageUrl, RentedHouse.ImageUrl);
        }
    }
}
