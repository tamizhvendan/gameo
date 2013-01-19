using System;
using System.Collections.Generic;
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
            var data = new List<TrendChartData>();
            var random = new Random();
            
            for (var i = 1; i < 8; i++)
            {
                var trendChartData = new TrendChartData
                                         {
                                             name = "Gaming Console " + i
                                         };
                for (var j = 0; j < 6; j++)
                {
                    trendChartData.data[j] = random.Next(20);
                }
                data.Add(trendChartData);
            }
            return Json(data);
        }
    }

    class TrendChartData
    {
        public string name { get; set; }
        public int[] data { get; set; }

        public TrendChartData()
        {
            name = string.Empty;
            data = new int[6];
        }
    }
}