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
            var currentTime = DateTime.Now.ToIST();
            var fooGameOnBranch1 = new Game { BranchName = "Branch1", InTime = currentTime, OutTime = currentTime.AddHours(1), CustomerName = "foo" };
            var fooGameOnBranch2 = new Game { BranchName = "Branch2", CustomerName = "foo" };
            var barGameOnBranch1 = new Game
                              {
                                  BranchName = "Branch1",
                                  InTime = currentTime.Subtract(new TimeSpan(0, 2, 0)),
                                  OutTime = currentTime.Subtract(new TimeSpan(0, 1, 0)),
                                  CustomerName = "bar"
                              };
            var fooBarGameOnBranch1 = new Game
                                 {
                                     BranchName = "Branch1",
                                     CustomerName = "foobar",
                                     InTime = currentTime.Subtract(new TimeSpan(0, 1, 0)),
                                     OutTime = currentTime.AddMinutes(30)
                                 };
            AddEntityToDatabase(barGameOnBranch1, fooBarGameOnBranch1, fooGameOnBranch2, fooGameOnBranch1);

            var nonCompletedGamesInBranch1 = gameRepository.GetNonCompletedGames("Branch1", currentTime);
            var nonCompletedGamesInBranch2 = gameRepository.GetNonCompletedGames("Branch2", currentTime);

            nonCompletedGamesInBranch1.Count().ShouldEqual(2);
            nonCompletedGamesInBranch2.Count().ShouldEqual(1);
            nonCompletedGamesInBranch2.First().CustomerName.ShouldEqual(fooGameOnBranch2.CustomerName);
            nonCompletedGamesInBranch1.Any(game => game.CustomerName == fooGameOnBranch1.CustomerName).ShouldBeTrue();
            nonCompletedGamesInBranch1.Any(game => game.CustomerName == fooBarGameOnBranch1.CustomerName).ShouldBeTrue();
        }

        [Test]
        public void Retrieves_Completed_Games_Within_given_day_for_given_branch()
        {
            var currentDateTime = DateTime.Now.ToIST();
            var branchName = "branch1";
            var completedGameOnBranch1 = new Game
                                              {
                                                  BranchName = branchName,
                                                  CustomerName = "foo",
                                                  InTime = currentDateTime.Subtract(new TimeSpan(0, 2, 0, 0)),
                                                  OutTime = currentDateTime.Subtract(new TimeSpan(0, 1, 0, 0))
                                              };
            var yesterDayCompletedGameOnBranch1 = new Game
                                                       {
                                                           BranchName = branchName,
                                                           InTime = currentDateTime.Subtract(new TimeSpan(1, 4, 0, 0)),
                                                           OutTime = currentDateTime.Subtract(new TimeSpan(1, 3, 0, 0))
                                                       };
            var nonCompletedGameOnBranch1 = new Game
                                                 {
                                                     BranchName = branchName,
                                                     InTime = currentDateTime,
                                                     OutTime = currentDateTime.AddHours(3)
                                                 };
            var gameOnBranch2 = new Game {BranchName = "Branch2"};
            AddEntityToDatabase(completedGameOnBranch1, yesterDayCompletedGameOnBranch1, nonCompletedGameOnBranch1, gameOnBranch2);

            var completedGames = gameRepository.GetCompletedGamesWithinGivenDay(branchName, currentDateTime).ToList();
            if (currentDateTime.Day == completedGameOnBranch1.OutTime.Day)
            {
                completedGames.Count().ShouldEqual(1);
                completedGames.First().BranchName.ShouldEqual(branchName);
                completedGames.First().CustomerName.ShouldEqual("foo");
            }
            else
            {
                completedGames.Count().ShouldEqual(0);
            }
        }

        [Test]
        public void Retrieves_Games_Played_withing_given_timeframe_for_given_branch()
        {
            var currentTime = DateTime.Now.ToIST();
            const string branch1Name = "Branch1";
            var fooGameOnBranch1 = new Game { BranchName = branch1Name, InTime = currentTime, OutTime = currentTime.AddHours(1), CustomerName = "foo" };
            var fooGameOnBranch2 = new Game { BranchName = "Branch2", CustomerName = "foo" };
            var barGameOnBranch1 = new Game
            {
                BranchName = branch1Name,
                InTime = currentTime.Subtract(new TimeSpan(0, 2, 0)),
                OutTime = currentTime.Subtract(new TimeSpan(0, 1, 0)),
                CustomerName = "bar"
            };
            var foobarGameOnBranch1 = new Game
            {
                BranchName = branch1Name,
                InTime = currentTime.AddDays(1),
                OutTime = currentTime.AddDays(1).AddHours(2)
            };

            AddEntityToDatabase(fooGameOnBranch1, fooGameOnBranch2, barGameOnBranch1, foobarGameOnBranch1);

            var games = gameRepository.GetGames(branch1Name, currentTime.Subtract(new TimeSpan(0, 3, 0)), currentTime.AddHours(5)).ToList();

            games.Count().ShouldEqual(2);
            games.All(game => game.BranchName == branch1Name).ShouldBeTrue();
            games.Any(game => game.CustomerName == "foo").ShouldBeTrue();
            games.Any(game => game.CustomerName == "bar").ShouldBeTrue();
        }

        [Test]
        public void Retrieves_Games_Started_On_a_given_day()
        {
            var currentTime = DateTime.Now.ToIST();
            const string branch1Name = "Branch1";
            var fooGameOnBranch1 = new Game { BranchName = branch1Name, InTime = currentTime, OutTime = currentTime.AddHours(1), CustomerName = "foo" };
            var fooNextDayGameOnBranch1 = new Game { BranchName = branch1Name, InTime = currentTime.AddDays(1), OutTime = currentTime.AddDays(1).AddHours(1), CustomerName = "foo" };
            var fooGameOnBranch2 = new Game { BranchName = "Branch2", CustomerName = "foo" };
            var barGameOnBranch1 = new Game
            {
                BranchName = branch1Name,
                InTime = currentTime.Subtract(new TimeSpan(1, 2, 0, 0)),
                OutTime = currentTime.Subtract(new TimeSpan(1, 1, 0, 0)),
                CustomerName = "bar"
            };
            AddEntityToDatabase(barGameOnBranch1, fooGameOnBranch2, fooGameOnBranch1, fooNextDayGameOnBranch1);

            var games = gameRepository.GetGames(branch1Name, currentTime);
            
            games.Count().ShouldEqual(1);
            games.First().CustomerName.ShouldEqual("foo");
        }
    }


}