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
    [TestFixture]
    public class GameServiceSpec
    {
        private Mock<IGamingConsoleRepository> gamingConsoleRepositoryMock;
        private Mock<IGameRepository> gameRepositoryMock;
        private Mock<IMembershipRepository> membershipRepositoryMock;
        private Mock<IGamingTrend> gamingTrendMock; 
        private DateTime currentTime;
        private GameService gameService;
        private string nameOfConsole1 = "Console1";
        private string nameOfConsole2 = "Console2";
        private string nameOfConsole3 = "Console3";
        private string nameOfConsole4 = "Console4";
        private List<GamingConsole> gamingConsoles;
        private List<Game> games;
        private TrendRequest trendRequest;

        [SetUp]
        public void SetUp()
        {
            currentTime = DateTime.UtcNow.ToIST();
            trendRequest = new TrendRequest { BranchName = "foo", From = DateTime.UtcNow, To = DateTime.UtcNow.AddDays(5) };
            gamingConsoles = new List<GamingConsole>
                                 {
                                     new GamingConsole {BranchName = "Branch1", Name = nameOfConsole1},
                                     new GamingConsole {BranchName = "Branch2", Name = nameOfConsole2},
                                     new GamingConsole {BranchName = "Branch1", Name = nameOfConsole3},
                                     new GamingConsole {BranchName = "Branch2", Name = nameOfConsole4},
                                 };
            games = new List<Game>
                        {
                            new Game
                                {
                                    BranchName = "Branch2",
                                    ConsoleName = nameOfConsole2,
                                    InTime = currentTime,
                                    OutTime = currentTime.AddHours(1),
                                    CustomerName = "foo"
                                },
                            new Game
                                {
                                    BranchName = "Branch2",
                                    ConsoleName = nameOfConsole2,
                                    InTime = currentTime,
                                    OutTime = currentTime.AddHours(2),
                                    CustomerName = "bar"
                                },
                            new Game
                                {
                                    BranchName = "Branch1",
                                    ConsoleName = nameOfConsole3,
                                    InTime = currentTime,
                                    OutTime = currentTime.AddHours(1),
                                    CustomerName = "foo"
                                },
                            new Game
                                {
                                    BranchName = "Branch1",
                                    ConsoleName = nameOfConsole3,
                                    InTime = currentTime,
                                    OutTime = currentTime.AddHours(2),
                                    CustomerName = "bar"
                                }
                        };
            gamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            gameRepositoryMock = new Mock<IGameRepository>();
            membershipRepositoryMock = new Mock<IMembershipRepository>();
            gamingTrendMock = new Mock<IGamingTrend>();

            gamingConsoleRepositoryMock
                .Setup(repo => repo.GetGamingConsolesByBranchName("Branch1"))
                .Returns(gamingConsoles.Where(console => console.BranchName == "Branch1"));
            gamingConsoleRepositoryMock
                .Setup(repo => repo.GetGamingConsolesByBranchName("Branch2"))
                .Returns(gamingConsoles.Where(console => console.BranchName == "Branch2"));

            gameService = new GameService(gameRepositoryMock.Object, gamingConsoleRepositoryMock.Object, membershipRepositoryMock.Object, gamingTrendMock.Object);
        }

        [Test]
        public void Retrieves_Status_of_all_non_completed_games_for_given_branch_name_Branch1()
        {
            gameRepositoryMock
                .Setup(repo => repo.GetNonCompletedGames("Branch1", currentTime)).Returns(games.Where(game => game.ConsoleName == nameOfConsole3));

            var nonCompletedGameStatuses = gameService.GetNonCompletedGamesStatus("Branch1", currentTime).ToList();

            nonCompletedGameStatuses.Count().ShouldEqual(2);
            var gamesOnConsole1 = nonCompletedGameStatuses.First(status => status.GamingConsoleName == nameOfConsole1);
            gamesOnConsole1.GamingConsoleName.ShouldEqual(nameOfConsole1);
            gamesOnConsole1.Players.Count().ShouldEqual(0);
            var gamesOnConsole3 = nonCompletedGameStatuses.First(status => status.GamingConsoleName == nameOfConsole3);
            gamesOnConsole3.GamingConsoleName.ShouldEqual(nameOfConsole3);
            gamesOnConsole3.Players.Count().ShouldEqual(2);
            var playByFoo = gamesOnConsole3.Players.First(player => player.CustomerName == "foo");
            playByFoo.CustomerName.ShouldEqual("foo");
            playByFoo.InTime.ShouldEqual(currentTime);
            playByFoo.OutTime.ShouldEqual(currentTime.AddHours(1));
        }

        [Test]
        public void Retrieves_Status_of_all_non_completed_games_for_given_branch_name_Branch2()
        {
            gameRepositoryMock
                .Setup(repo => repo.GetNonCompletedGames("Branch2", currentTime)).Returns(games.Where(game => game.ConsoleName == nameOfConsole2));

            var nonCompletedGameStatuses = gameService.GetNonCompletedGamesStatus("Branch2", currentTime).ToList();

            nonCompletedGameStatuses.Count().ShouldEqual(2);
            var gameOnConsole4 = nonCompletedGameStatuses.First(status => status.GamingConsoleName == nameOfConsole4);
            gameOnConsole4.GamingConsoleName.ShouldEqual(nameOfConsole4);
            gameOnConsole4.Players.Count().ShouldEqual(0);
            var gameOnConsole2 = nonCompletedGameStatuses.First(status => status.GamingConsoleName == nameOfConsole2);
            gameOnConsole2.GamingConsoleName.ShouldEqual(nameOfConsole2);
            gameOnConsole2.Players.Count().ShouldEqual(2);
            var playByBar = gameOnConsole2.Players.First(player => player.CustomerName == "bar");
            playByBar.CustomerName.ShouldEqual("bar");
            playByBar.InTime.ShouldEqual(currentTime);
            playByBar.OutTime.ShouldEqual(currentTime.AddHours(2));
        }


        [Test]
        public void AssignConsoleForMembership_adds_game_in_game_repository()
        {
            var membership = new Membership();
            var game = new Game();
            gameRepositoryMock.Setup(repo => repo.Add(game)).Verifiable();

            gameService.AssignConsoleForMembership(membership, game);

            gameRepositoryMock.Verify(repo => repo.Add(game));
            game.MembershipId.ShouldEqual(membership.MembershipId);
        }

        [Test]
        public void AssignConsoleForMembership_update_added_game_with_membership_id()
        {
            var membership = new Membership();
            var game = new Game();

            gameService.AssignConsoleForMembership(membership, game);

            game.MembershipId.ShouldEqual(membership.MembershipId);
        }

        [Test]
        public void AssignConsoleForMembership_updates_membership_with_the_game_passed()
        {
            var membership = new Membership();
            var game = new Game();
            membershipRepositoryMock.Setup(repo => repo.Update(membership)).Verifiable();

            gameService.AssignConsoleForMembership(membership, game);

            membership.Games.Count.ShouldEqual(1);
            membership.Games.First().ShouldEqual(game);
            membershipRepositoryMock.Verify(repo => repo.Update(membership));
            game.MembershipId.ShouldEqual(membership.MembershipId);
        }

        [Test]
        public void MarkGameAsInvalid_retrieve_game_from_repository_and_updates_it_as_invalid()
        {
            var game = new Game();
            gameRepositoryMock.Setup(repo => repo.GetById(game.Id)).Returns(game);
            gameRepositoryMock.Setup(repo => repo.Update(game)).Verifiable();

            gameService.MarkGameAsInvalid(game.Id);

            game.IsValid.ShouldBeFalse();
            gameRepositoryMock.Verify(repo => repo.Update(game));
        }

        [Test]
        public void ForMembershipGame_MarkGameAsInvalid_invalidate_both_game_and_game_in_membership()
        {
            var membership = new Membership();
            var game = new Game {MembershipId = membership.MembershipId};
            membership.AddGame(game);
            membershipRepositoryMock.Setup(repo => repo.FindByMembershipId(membership.MembershipId)).Returns(membership);
            gameRepositoryMock.Setup(repo => repo.GetById(game.Id)).Returns(game);
            gameRepositoryMock.Setup(repo => repo.Update(game)).Verifiable();
            membershipRepositoryMock.Setup(repo => repo.Update(membership)).Verifiable();

            gameService.MarkGameAsInvalid(game.Id);

            gameRepositoryMock.Verify(repo => repo.Update(game));
            membershipRepositoryMock.Verify(repo => repo.Update(membership));
            game.IsValid.ShouldBeFalse();
        }

        [Test]
        public void GetGamingTrends_retrieve_games_from_game_repository_for_given_date_range_and_branch()
        {
            gameRepositoryMock.Setup(repo => repo.GetGames(trendRequest.BranchName, trendRequest.From, trendRequest.To)).Returns(games);

            gameService.GetGamingTrends(trendRequest);

            gameRepositoryMock.Verify(repo => repo.GetGames(trendRequest.BranchName, trendRequest.From, trendRequest.To));
        }

        [Test]
        public void GetGamingTrends_compute_gaming_trends_using_GamingTrend()
        {
            var buckets = new[] { new Bucket<Game>() };
            gameRepositoryMock.Setup(repo => repo.GetGames(trendRequest.BranchName, trendRequest.From, trendRequest.To)).Returns(games);
            gamingTrendMock.Setup(trend => trend.Compute(games)).Returns(buckets);

            var gamingTrends = gameService.GetGamingTrends(trendRequest);

            gamingTrends.ShouldEqual(buckets);
        }

        [Test]
        public void GetGamePriceTrends_compute_gaming_price_trends_from_game_buckets()
        {
            var bucket1 = new Bucket<Game> {Label = "9-11", Values = new[] {new Game {Price = 40}, new Game {Price = 40}}};
            var bucket2 = new Bucket<Game> {Label = "11-13", Values = new[] {new Game {Price = 20}, new Game {Price = 30}}};

            var gamePriceTrends = gameService.GetGamePriceTrends(new[] {bucket1, bucket2}).ToList();

            gamePriceTrends.Count.ShouldEqual(2);
            gamePriceTrends.First().TimeDurationLabel.ShouldEqual("9-11");
            gamePriceTrends.First().Price.ShouldEqual(80);
            gamePriceTrends.Last().TimeDurationLabel.ShouldEqual("11-13");
            gamePriceTrends.Last().Price.ShouldEqual(50);
        }
    }
}