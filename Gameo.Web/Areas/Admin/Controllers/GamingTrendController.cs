using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Web.Areas.Admin.Models;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class GamingTrendController : ApplicationControllerBase
    {
        private readonly IBranchRepository branchRepository;

        public GamingTrendController(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        public ViewResult Index()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);   
            return View();
        }

        [HttpPost]
        public JsonResult GetTrend(TrendRequest trendRequest)
        {
            return Json(trendRequest);
        }
    }
}