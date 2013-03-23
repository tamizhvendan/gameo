using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class GamingTrendSpec
    {
        private List<Game> games;

        private IEnumerable<Game> GetGamesStartedBetween(int startTimeInHours, int endTimeInHours)
        {
            return new[]
                {   
                    new Game {InTime = new DateTime(2011, 1, 1, startTimeInHours, 0, 0)},
                    new Game {InTime = new DateTime(2011, 5, 1, startTimeInHours, 54, 0)},
                    new Game {InTime = new DateTime(2011, 5, 8, endTimeInHours - 1, 15, 0)},
                    new Game {InTime = new DateTime(2011, 10, 10, endTimeInHours - 1, 59, 0)}
                };
        }
            
        [SetUp]
        public void SetUp()
        {
            games = new List<Game>();
            games.AddRange(GetGamesStartedBetween(9, 11));
            games.AddRange(GetGamesStartedBetween(11, 13));
            games.AddRange(GetGamesStartedBetween(13, 15));
            games.AddRange(GetGamesStartedBetween(15, 17));
            games.AddRange(GetGamesStartedBetween(17, 19));
            games.AddRange(GetGamesStartedBetween(19, 22));
        }

        [Test]
        [TestCase("9-11",9, 11)]
        [TestCase("11-13", 11, 13)]
        [TestCase("13-15", 13, 15)]
        [TestCase("15-17", 15, 17)]
        [TestCase("17-19", 17, 19)]
        [TestCase("19-22", 19, 22)]
        public void Can_classify_games_started_between_given_time_range(string timeRange, int startTimeInHours, int endTimeInHours)
        {
            var gamingTrend = new GamingTrend();

            var bucketFor9to11 = gamingTrend.Compute(games).First(bucket => bucket.Label == timeRange);

            bucketFor9to11.Values.All(game => game.InTime.Hour >= startTimeInHours && game.InTime.Hour < endTimeInHours).ShouldBeTrue();
        }
    }
}