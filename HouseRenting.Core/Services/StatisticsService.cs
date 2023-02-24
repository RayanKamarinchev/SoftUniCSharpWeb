using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models.Statistics;
using HouseRenting.Data;

namespace HouseRenting.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HouseRentingDbContext context;

        public StatisticsService(HouseRentingDbContext _context)
        {
            context = _context;
        }
        public StatisticsServiceModel Total()
        {
            return new StatisticsServiceModel()
            {
                TotalHouses = context.Houses.Count(),
                TotalRents = context.Houses.Count(h => h.RenterId != null)
            };
        }
    }
}
