using HouseRenting.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Test.Mocks
{
    public static class DbMock
    {
        public static HouseRentingDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<HouseRentingDbContext>()
                    .UseInMemoryDatabase("HouseRentingInMemoryDb" + DateTime.Now.Ticks.ToString())
                    .Options;
                return new HouseRentingDbContext(dbContextOptions, false);
            }
        }
    }
}
