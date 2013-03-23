using System.Linq;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Models;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.GamingTrendControllerSpecs
{
    [TestFixture]
    public class GetTrendActionSpec : GamingTrendControllerSpecBase
    {
        private TrendRequest trendRequest;

        [SetUp]
        public void SetUp()
        {
            trendRequest = new TrendRequest();
        }

        [Test]
        public void ComputeTrend_using_GameService()
        {
            GameServiceMock.Setup(service => service.GetGamingTrends(trendRequest)).Verifiable();

            GamingTrendController.GetTrend(trendRequest);

            GameServiceMock.Verify(service => service.GetGamingTrends(trendRequest));
        }

        [Test]
        public void Transform_buckets_of_game_to_list_of_trend_chart_datum_using_TrendChartEngine()
        {
            var buckets = Enumerable.Empty<Bucket<Game>>();
            var trendChartData = Enumerable.Empty<TrendChartDatum>();
            GameServiceMock.Setup(service => service.GetGamingTrends(trendRequest)).Returns(buckets);
            TrendChartEngineMock.Setup(engine => engine.Transform(buckets)).Returns(trendChartData);

            var jsonResult = GamingTrendController.GetTrend(trendRequest);

            jsonResult.Data.ShouldEqual(trendChartData);
        }
    }
}