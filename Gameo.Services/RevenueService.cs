using System;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly IGameRepository gameRepository;
        private readonly IMembershipRepository membershipRepository;
        private readonly IDailySaleDetailsRepository dailySaleDetailsRepository;
        private readonly IMonthlyExpensesRepository monthlyExpensesRepository;

        public RevenueService(IGameRepository gameRepository, IMembershipRepository membershipRepository, 
            IDailySaleDetailsRepository dailySaleDetailsRepository, IMonthlyExpensesRepository monthlyExpensesRepository)
        {
            this.gameRepository = gameRepository;
            this.membershipRepository = membershipRepository;
            this.dailySaleDetailsRepository = dailySaleDetailsRepository;
            this.monthlyExpensesRepository = monthlyExpensesRepository;
        }

        public MonthlyRevenue ComputeMonthlyRevenue(string branchName, int year, int month)
        {
            var dateTime = new DateTime(year, month, 1);

            var oneTimeGames = gameRepository
                            .GetGames(branchName, dateTime.FirstDayOfMonth(), dateTime.LastDayOfMonth())
                            .Where(game => game.GamePaymentType == GamePaymentType.OneTime)
                            .ToList();

            var packageGames = oneTimeGames.Where(game => game.PackageType != PackageType.No_Package);
            var nonPackageGames = oneTimeGames.Where(game => game.PackageType == PackageType.No_Package);

            var membershipReCharges = membershipRepository.GetRecharges(branchName, dateTime.FirstDayOfMonth(), dateTime.LastDayOfMonth()).ToList();

            return new MonthlyRevenue
                       {
                           DateTime = dateTime,
                           NonMembershipGamesCount = oneTimeGames.Count,
                           MembershipRechargesCount = membershipReCharges.Count,
                           RevenueByNonPackageOneTimeGames = nonPackageGames.Sum(game => game.Price),
                           RevenueByPackageOneTimeGames = packageGames.Sum(game => game.Price),
                           RevenueByMembershipRecharges = membershipReCharges.Sum(recharge => recharge.Price),
                           EbMeterReading = dailySaleDetailsRepository.GetEbMeterReadingForTheMonth(branchName, year, month),
                           MonthlyExpense = monthlyExpensesRepository.GetMonthlyExpenses(branchName, month, year)
                       };
        }
    }
}