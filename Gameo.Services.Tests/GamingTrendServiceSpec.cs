using System;
using Gameo.DataAccess.Core;
using Moq;
using NUnit.Framework;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class GamingTrendServiceSpec
    {
        private Mock<IGameRepository> gameRepositoryMock;
        private Mock<IGamingConsoleRepository> gamingConsoleRepositoryMock;
        private GamingTrendService gamingTrendService;
        private DateTime startDate;
        private DateTime endDate;
        private string branchName;

        [SetUp]
        public void SetUp()
        {
            gamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            gameRepositoryMock = new Mock<IGameRepository>();
            gamingTrendService = new GamingTrendService(gameRepositoryMock.Object, gamingConsoleRepositoryMock.Object);
            startDate = new DateTime(2011, 1, 1);
            endDate = new DateTime(2011, 31, 1);
            branchName = "Branch1";
        }

        [Test]
        public void GetGamingTrendForAllConsoles_Returns_GamingTrends_of_all_consoles_for_given_period_of_branch()
        {
            gamingTrendService.GetGamingTrendForAllConsoles(startDate, endDate, branchName);
        }
    }
}