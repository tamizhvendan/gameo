using System;
using System.Linq;
using Gameo.Domain;
using Gameo.Services;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.RevenueControllerSpecs
{
    [TestFixture]
    public class GetLastSevenMonthRevenueSpec : RevenueControllerSpecBase
    {
        [Test]
        public void Retrieves_seven_month_monthly_revenue_from_Revenue_service()
        {
            var currentTime = DateTime.UtcNow.ToIST();
            var lastSevenMonths = currentTime.LastSevenMonths().ToList();
            var branchName = "foo";
            var monthlyRevenues = lastSevenMonths.Select(dateTime => new MonthlyRevenue {DateTime = dateTime}).ToList();
            for (int i = 0; i < lastSevenMonths.Count; i++)
            {
                RevenueServiceMock
                    .Setup(repo => repo.ComputeMonthlyRevenue(branchName, lastSevenMonths[i].Year, lastSevenMonths[i].Month))
                    .Returns(monthlyRevenues[i]);
            }

            var jsonResult = RevenueController.GetMonthlyRevenue(branchName);

            jsonResult.Data.ShouldEqual(monthlyRevenues);
        }
    }
}