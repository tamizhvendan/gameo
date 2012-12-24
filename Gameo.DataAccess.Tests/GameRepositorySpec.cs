using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class GameRepositorySpec : RepositorySpecBase<Game>
    {
        private GameRepository gameRepository;

        [SetUp]
        public void SetUp()
        {
            gameRepository = new GameRepository();
        }

        [Test]
        public void Adds_many_game_entities()
        {
            var games = new List<Game>
                            {
                                new Game
                                    {
                                        ConsoleName = "Console1"
                                    },
                                new Game
                                    {
                                        ConsoleName = "Console2"
                                    }
                            };

            gameRepository.AddMany(games);

            AssertNewlyAddedManyEntities(entities =>
                                             {
                                                 entities.Count().ShouldEqual(2);
                                                 entities.First().ConsoleName.ShouldEqual("Console1");
                                                 entities.Last().ConsoleName.ShouldEqual("Console2");
                                             });
        }

        [Test]
        public void Retrieves_Non_Completed_Games_of_given_console()
        {
            var currentTime = DateTime.Now;
            var fooGame = new Game { ConsoleName = "Console1", InTime = currentTime, OutTime = currentTime.AddHours(1), CustomerName = "foo" };
            var fooGameOnConsole2 = new Game { ConsoleName = "Console2", CustomerName = "foo" };
            var barGame = new Game
                              {
                                  ConsoleName = "Console1", 
                                  InTime = currentTime.Subtract(new TimeSpan(0, 2, 0)), 
                                  OutTime = currentTime.Subtract(new TimeSpan(0, 1, 0)), 
                                  CustomerName = "bar"
                              };
            var fooBarGame = new Game
                                 {
                                     ConsoleName = "Console1",
                                     CustomerName = "foobar",
                                     InTime = currentTime.Subtract(new TimeSpan(0, 1, 0)),
                                     OutTime = currentTime.AddMinutes(30)
                                 };
            AddEntityToDatabase(barGame, fooBarGame, fooGameOnConsole2, fooGame);

            var nonCompletedGamesInConsole1 = gameRepository.GetNonCompletedGames("Console1", currentTime);
            var nonCompletedGamesInConsole2 = gameRepository.GetNonCompletedGames("Console2", currentTime);

            nonCompletedGamesInConsole1.Count().ShouldEqual(2);
            nonCompletedGamesInConsole2.Count().ShouldEqual(1);
            nonCompletedGamesInConsole2.First().CustomerName.ShouldEqual(fooGameOnConsole2.CustomerName);
            nonCompletedGamesInConsole1.Any(game => game.CustomerName == fooGame.CustomerName).ShouldBeTrue();
            nonCompletedGamesInConsole1.Any(game => game.CustomerName == fooBarGame.CustomerName).ShouldBeTrue();
        }
    }


}