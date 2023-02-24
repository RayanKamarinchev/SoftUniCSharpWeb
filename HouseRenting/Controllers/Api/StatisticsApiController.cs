using HouseRenting.Core.Contracts;
using HouseRenting.Core.Models.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Web.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService _statisticsService)
        {
            statisticsService = _statisticsService;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            return statisticsService.Total();
        }
    }
}
