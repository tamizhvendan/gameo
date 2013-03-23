using Gameo.Services;
using Gameo.Web.Areas.Admin.Controllers;
using Gameo.Web.Models;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.GamingTrendControllerSpecs
{
    public abstract class GamingTrendControllerSpecBase : ControllerSpecBase
    {
        protected GamingTrendController GamingTrendController;
        protected Mock<IGameService> GameServiceMock;
        protected Mock<ITrendChartEngine> TrendChartEngineMock;

        [SetUp]
        public void GamingTrendControllerSpecInit()
        {
            GameServiceMock = new Mock<IGameService>();
            TrendChartEngineMock = new Mock<ITrendChartEngine>();
            GamingTrendController = new GamingTrendController(BranchRepositoryMock.Object, GameServiceMock.Object, TrendChartEngineMock.Object);
        }
    }
}