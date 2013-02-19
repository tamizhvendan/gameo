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
                                             Name = "Gaming Console " + i
                                         };
                for (var j = 0; j < 6; j++)
                {
                    trendChartData.Data[j] = random.Next(20);
                }
                data.Add(trendChartData);
            }
            return Json(data);
        }
    }

    class TrendChartData
    {
        public string Name { get; set; }
        public int[] Data { get; set; }

        public TrendChartData()
        {
            Name = string.Empty;
            Data = new int[6];
        }
    }
}