using System;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class MonthlyRevenueSpec
    {
        [Test]
        public void UserFriendlyMonthlyString_displays_abbreviated_month_with_year()
        {
            var janMonthlyRevenue = new MonthlyRevenue {DateTime = new DateTime(2011, 1, 1)};
            var decMonthlyRevenue = new MonthlyRevenue {DateTime = new DateTime(2011, 12, 1)};

            janMonthlyRevenue.UserFriendlyMonthlyString.ShouldEqual("Jan-2011");
            decMonthlyRevenue.UserFriendlyMonthlyString.ShouldEqual("Dec-2011");
        }

        [Test]
        public void TotalCollection_is_sum_of_Revenues_from_games_and_membership_recharges()
        {
            var monthlyRevenue = new MonthlyRevenue {RevenueByNonPackageOneTimeGames = 10, RevenueByMembershipRecharges = 20};

            monthlyRevenue.TotalCollection.ShouldEqual(30);
        }
    }
}