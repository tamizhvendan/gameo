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

        public RevenueService(IGameRepository gameRepository, IMembershipRepository membershipRepository)
        {
            this.gameRepository = gameRepository;
            this.membershipRepository = membershipRepository;
        }

        public MonthlyRevenue ComputeMonthlyRevenue(string branchName, int year, int month)
        {
            var dateTime = new DateTime(year, month, 1);

            var games = gameRepository
                            .GetGames(branchName, dateTime.FirstDayOfMonth(), dateTime.LastDayOfMonth())
                            .Where(game => game.GamePaymentType == GamePaymentType.OneTime )
                            .ToList();

            var membershipReCharges = membershipRepository.GetRecharges(branchName, dateTime.FirstDayOfMonth(), dateTime.LastDayOfMonth()).ToList();

            return new MonthlyRevenue
                       {
                           DateTime = dateTime,
                           NonMembershipGamesCount = games.Count,
                           MembershipRechargesCount = membershipReCharges.Count,
                           RevenueByGames = games.Sum(game => game.Price),
                           RevenueByMembershipRecharges = membershipReCharges.Sum(recharge => recharge.Price)
                       };
        }
    }
}