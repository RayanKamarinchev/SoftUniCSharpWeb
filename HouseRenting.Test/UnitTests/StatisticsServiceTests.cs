using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Contracts;
using HouseRenting.Core.Services;
using NUnit.Framework;

namespace HouseRenting.Test.UnitTests
{
    [TestFixture]
    public class StatisticsServiceTests : UnitTestBase
    {
        private IStatisticsService statisticsService;

        [OneTimeSetUp]
        public void SetUp()
        {
            statisticsService = new StatisticsService(context);
        }

        [Test]
        public void Total_Correct()
        {
            var result = statisticsService.Total();
            Assert.IsNotNull(result);
            int houseCount = context.Houses.Count();
            Assert.AreEqual(result.TotalHouses, houseCount);
            int rentsCount = context.Houses.Count(h => h.RenterId != null);
            Assert.AreEqual(result.TotalRents, rentsCount);
        }
    }
}
