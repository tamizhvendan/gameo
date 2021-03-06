﻿using System;
using Gameo.Domain;

namespace Gameo.Services
{
    public class MonthlyRevenue
    {
        public DateTime DateTime { get; set; }
        public decimal RevenueByNonPackageOneTimeGames { get; set; }
        public decimal RevenueByMembershipRecharges { get; set; }
        public int NonMembershipGamesCount { get; set; }
        public int MembershipRechargesCount { get; set; }

        public string UserFriendlyMonthlyString
        {
            get { return DateTime.ToString("MMM-yyyy"); }
        }

        public decimal TotalCollection
        {
            get { return RevenueByNonPackageOneTimeGames + RevenueByMembershipRecharges; }
        }

        public decimal EbMeterReading { get; set; }

        public decimal RevenueByPackageOneTimeGames { get; set; }

        public MonthlyExpense MonthlyExpense { get; set; }
    }
}