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
    public class GameStatusServiceSpec
    {
        private Mock<IGamingConsoleRepository> gamingConsoleRepositoryMock;
        private Mock<IGameRepository> gameRepositoryMock;
        private DateTime currentTime;
        private GameStatusService gameStatusService;
        private string nameOfConsole1 = "Console1";
        private string nameOfConsole2 = "Console2";
        private string nameOfConsole3 = "Console3";
        private string nameOfConsole4 = "Console4";

        [SetUp]
        public void SetUp()
        {
            currentTime = DateTime.Now;
            var gamingConsoles = new List<GamingConsole>
                                     {
                                         new GamingConsole {BranchName = "Branch1", Name = nameOfConsole1},
                                         new GamingConsole {BranchName = "Branch2", Name = nameOfConsole2},
                                         new GamingConsole {BranchName = "Branch1", Name = nameOfConsole3},
                                         new GamingConsole {BranchName = "Branch2", Name = nameOfConsole4},
                                     };
            var games = new List<Game>
                            {
                                new Game
                                    {
                                        ConsoleName = nameOfConsole2,
                                        InTime = currentTime,
                                        OutTime = currentTime.AddHours(1),
                                        CustomerName = "foo"
                                    },
                                new Game
                                    {
                                        ConsoleName = nameOfConsole2,
                                        InTime = currentTime,
                                        OutTime = currentTime.AddHours(2),
                                        CustomerName = "bar"
                                    },
                                new Game
                                    {
                                        ConsoleName = nameOfConsole3,
                                        InTime = currentTime,
                                        OutTime = currentTime.AddHours(1),
                                        CustomerName = "foo"
                                    },
                                new Game
                                    {
                                        ConsoleName = nameOfConsole3,
                                        InTime = currentTime,
                                        OutTime = currentTime.AddHours(2),
                                        CustomerName = "bar"
                                    }
                            };
            gamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            gameRepositoryMock = new Mock<IGameRepository>();

            gamingConsoleRepositoryMock
                .Setup(repo => repo.GetGamingConsolesByBranchName("Branch1"))
                .Returns(gamingConsoles.Where(console => console.BranchName == "Branch1"));
            gamingConsoleRepositoryMock
                .Setup(repo => repo.GetGamingConsolesByBranchName("Branch2"))
                .Returns(gamingConsoles.Where(console => console.BranchName == "Branch2"));

            gameRepositoryMock
                .Setup(repo => repo.GetNonCompletedGames(nameOfConsole1, currentTime)).Returns(Enumerable.Empty<Game>());
            gameRepositoryMock
                .Setup(repo => repo.GetNonCompletedGames(nameOfConsole3, currentTime)).Returns(
                    games.Where(game => game.ConsoleName == nameOfConsole3));
            gameRepositoryMock
                .Setup(repo => repo.GetNonCompletedGames(nameOfConsole2, currentTime)).Returns(
                    games.Where(game => game.ConsoleName == nameOfConsole2));
            gameRepositoryMock
                .Setup(repo => repo.GetNonCompletedGames(nameOfConsole4, currentTime)).Returns(Enumerable.Empty<Game>());

            gameStatusService = new GameStatusService(gameRepositoryMock.Object, gamingConsoleRepositoryMock.Object);
        }

        [Test]
        public void Retrieves_Status_of_all_non_completed_games_for_given_branch_name_Branch1()
        {
            var nonCompletedGameStatuses = gameStatusService.GetNonCompletedGameStatuses("Branch1", currentTime);

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
            var nonCompletedGameStatuses = gameStatusService.GetNonCompletedGameStatuses("Branch2", currentTime);

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
    }
}