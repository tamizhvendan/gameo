using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Models;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.ModelSpecs
{
    public class TrendChartEngineSpec
    {
        [TestFixture]
        public class When_transforming_list_of_game_buckets_to_list_of_trend_chart_data
        {
            private string[] consoleNames;
            private IEnumerable<Game> games;
            private Bucket<Game>[] gameBuckets;
            private TrendChartEngine trendChartEngine;

            [SetUp]
            public void SetUp()
            {
                consoleNames = new[] { "Console1", "Console2", "Console3", "Console4" };
                games = consoleNames.Take(3).Select(consoleName => new Game { ConsoleName = consoleName });
                gameBuckets = new[]
                                  {
                                      new Bucket<Game> {Label = "9-11", Values = games},
                                      new Bucket<Game> {Label = "11-13", Values = games},
                                      new Bucket<Game> {Label = "13-15", Values = games.Take(2)},
                                      new Bucket<Game> {Label = "15-17", Values = new[] { new Game{ConsoleName = consoleNames.Last()}}}
                                  };
                trendChartEngine = new TrendChartEngine();
            }

            [Test]
            public void There_Should_be_exactly_one_trend_chart_datum_for_the_each_gaming_consoles()
            {
                var trendChartData = trendChartEngine.Transform(gameBuckets).ToList();

                trendChartData.Count.ShouldEqual(consoleNames.Count());
                consoleNames.All(consoleName => trendChartData.Any(trendChartDatum => trendChartDatum.Name == consoleName)).ShouldBeTrue();
            }

            [Test]
            public void The_data_in_each_trend_chart_datum_elements_count_should_match_buckets_count()
            {
                var trendChartData = trendChartEngine.Transform(gameBuckets).ToList();

                trendChartData.All(trendChartDatum => trendChartDatum.Data.Count() == gameBuckets.Count()).ShouldBeTrue();
            }

            [Test]
            public void The_data_in_each_trend_chart_datum_represents_gamingConsoles_presence_in_each_bucket()
            {
                var trendChartData = trendChartEngine.Transform(gameBuckets).ToList();

                trendChartData.First(trendChartDatum => trendChartDatum.Name == "Console1").Data.SequenceEqual(new[]{ 1, 1, 1, 0 }).ShouldBeTrue();
                trendChartData.First(trendChartDatum => trendChartDatum.Name == "Console2").Data.SequenceEqual(new[] { 1, 1, 1, 0 }).ShouldBeTrue();
                trendChartData.First(trendChartDatum => trendChartDatum.Name == "Console3").Data.SequenceEqual(new[] { 1, 1, 0, 0 }).ShouldBeTrue();
                trendChartData.First(trendChartDatum => trendChartDatum.Name == "Console4").Data.SequenceEqual(new[] { 0, 0, 0, 1 }).ShouldBeTrue();
            }
        }
    }
}