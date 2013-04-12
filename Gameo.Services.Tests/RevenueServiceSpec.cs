using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    public class RevenueServiceSpec
    {
        [TestFixture]
        public class GetMonthlyRevenue_for_given_month_and_year_for_given_branch
        {
            private RevenueService revenueService;
            private Mock<IGameRepository> gameRepositoryMock;
            private Mock<IMembershipRepository> membershipRepositoryMock;
            
            private DateTime currentTime;
            private IEnumerable<Game> games;
            private IEnumerable<MembershipReCharge> membershipReCharges;
            private Mock<IDailySaleDetailsRepository> dailySaleDetailsRepository;
            private const string BranchName = "branch1";

            [SetUp]
            public void SetUp()
            {
                gameRepositoryMock = new Mock<IGameRepository>();
                membershipRepositoryMock = new Mock<IMembershipRepository>();
                dailySaleDetailsRepository = new Mock<IDailySaleDetailsRepository>();
                revenueService = new RevenueService(gameRepositoryMock.Object, membershipRepositoryMock.Object, dailySaleDetailsRepository.Object);
                currentTime = DateTime.UtcNow.ToIST();
                games = Enumerable.Empty<Game>();
                membershipReCharges = Enumerable.Empty<MembershipReCharge>();
            }

            [Test]
            public void Retrieves_games_played_for_the_given_time_range_from_game_repository()
            {
                games = new [] { new Game { Price = 50} };
                gameRepositoryMock.Setup(repo => repo.GetGames(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth())).Returns(games);

                revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                gameRepositoryMock.Verify(repo => repo.GetGames(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth()));
            }

            [Test]
            public void Retrieves_membership_recharges_done_on_given_time_range_from_membership_repository()
            {
                var membershipReCharges = new[] {new MembershipReCharge()};
                membershipRepositoryMock.Setup(repo => repo.GetRecharges(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth())).Returns(membershipReCharges);

                revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                membershipRepositoryMock.Verify(repo => repo.GetRecharges(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth()));
            }

            [Test]
            public void Returns_MonthlyRevenue_with_DateTime_of_given_month_and_year()
            {
                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.DateTime.ShouldEqual(new DateTime(currentTime.Year, currentTime.Month, 1));
            }

            [Test]
            public void Returns_MonthlyRevenue_with_NonMembershipGamesCount()
            {
                games = new[] {new Game {Price = 15}, new Game {Price = 20}, new Game {Price = 40, GamePaymentType = GamePaymentType.Membership}};
                gameRepositoryMock.Setup(repo => repo.GetGames(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth())).Returns(games);

                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.NonMembershipGamesCount.ShouldEqual(2);
            }

            [Test]
            public void Returns_MonthlyRevenue_with_MembershipRechargesCount()
            {
                membershipReCharges = new[] {new MembershipReCharge(), new MembershipReCharge()};
                membershipRepositoryMock.Setup(repo => repo.GetRecharges(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth())).Returns(membershipReCharges);

                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.MembershipRechargesCount.ShouldEqual(2);
            }

            [Test]
            public void Returns_MonthlyRevenue_with_RevenueByNonPackageOneTimeGames()
            {
                games = new[] { new Game { Price = 15 }, new Game { Price = 20 }, new Game { Price = 40, GamePaymentType = GamePaymentType.Membership } };
                gameRepositoryMock.Setup(repo => repo.GetGames(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth())).Returns(games);

                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.RevenueByNonPackageOneTimeGames.ShouldEqual(35);
            }

            [Test]
            public void Returns_MonthlyRevenue_with_RevenueByPackageOneTimeGames()
            {
                games = new[] { new Game { Price = 15 }, new Game { Price = 20, PackageType = PackageType.Package_Of_3_Hours}, new Game { Price = 40, GamePaymentType = GamePaymentType.Membership } };
                gameRepositoryMock.Setup(repo => repo.GetGames(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth())).Returns(games);

                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.RevenueByPackageOneTimeGames.ShouldEqual(20);
            }

            [Test]
            public void Returns_MonthlyRevenue_with_RevenueByMembershipRecharges()
            {
                membershipReCharges = new[] {new MembershipReCharge {Price = 15}, new MembershipReCharge {Price = 15}};
                membershipRepositoryMock
                    .Setup(repo => repo.GetRecharges(BranchName, currentTime.FirstDayOfMonth(), currentTime.LastDayOfMonth()))
                    .Returns(membershipReCharges);

                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.RevenueByMembershipRecharges.ShouldEqual(30);
            }

            [Test]
            public void Returns_MonthlyRevenue_with_EbMeterReadingForThatMonth()
            {
                const decimal expectedEbMeterReading = 234m;
                dailySaleDetailsRepository.Setup(repo => repo.GetEbMeterReadingForTheMonth(BranchName, currentTime.Year, currentTime.Month))
                                          .Returns(expectedEbMeterReading);

                var monthlyRevenue = revenueService.ComputeMonthlyRevenue(BranchName, currentTime.Year, currentTime.Month);

                monthlyRevenue.EbMeterReading.ShouldEqual(expectedEbMeterReading);
            }
        }
    }
}