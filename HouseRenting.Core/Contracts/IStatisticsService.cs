using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Models.Statistics;

namespace HouseRenting.Core.Contracts
{
    public interface IStatisticsService
    {
        StatisticsServiceModel Total();
    }
}
