using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.Controllers;
using Gameo.Web.Models;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class GamingTrendController : ApplicationControllerBase
    {
        private readonly IBranchRepository branchRepository;
        private readonly IGameService gameService;
        private readonly ITrendChartEngine trendChartEngine;

        public GamingTrendController(IBranchRepository branchRepository, IGameService gameService, ITrendChartEngine trendChartEngine)
        {
            this.branchRepository = branchRepository;
            this.gameService = gameService;
            this.trendChartEngine = trendChartEngine;
        }

        public ViewResult Index()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);   
            return View();
        }

        [HttpPost]
        public JsonResult GetTrend(TrendRequest trendRequest)
        {
            var gamingTrends = gameService.GetGamingTrends(trendRequest);

            return Json(trendChartEngine.Transform(gamingTrends));
        }
    }
}