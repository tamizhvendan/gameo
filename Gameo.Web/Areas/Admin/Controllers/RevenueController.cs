using System;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class RevenueController : ApplicationControllerBase
    {
        private readonly IBranchRepository branchRepository;
        private readonly IRevenueService revenueService;

        public RevenueController(IBranchRepository branchRepository, IRevenueService revenueService)
        {
            this.branchRepository = branchRepository;
            this.revenueService = revenueService;
        }

        public ViewResult Index()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View();
        }

        public JsonResult GetMonthlyRevenueTrend(string branchName)
        {
            var lastSevenMonths = DateTime.UtcNow.ToIST().LastSevenMonths();
            
            var monthlyRevenues = lastSevenMonths.Select(dateTime => revenueService.ComputeMonthlyRevenue(branchName, dateTime.Year, dateTime.Month));

            return Json(monthlyRevenues, JsonRequestBehavior.AllowGet);
        }
    }
}