using System;

namespace Gameo.Services
{
    public class MonthlyRevenue
    {
        public DateTime DateTime { get; set; }
        public decimal RevenueByGames { get; set; }
        public decimal RevenueByMembershipRecharges { get; set; }
        public int NonMembershipGamesCount { get; set; }
        public int MembershipRechargesCount { get; set; }

        public string UserFriendlyMonthlyString
        {
            get { return DateTime.ToString("MMM-yyyy"); }
        }

        public decimal TotalCollection
        {
            get { return RevenueByGames + RevenueByMembershipRecharges; }
        }
    }
}