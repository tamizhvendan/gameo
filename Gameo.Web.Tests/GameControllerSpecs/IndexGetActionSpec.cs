using System;
using System.Linq;
using Gameo.Services;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : GameControllerSpecBase
    {
        [Test]
        public void Returs_Index_View()
        {
            var viewResult = GameController.Index(CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void Get_Non_CompletedGames_from_GameStatusService_Pass_them_as_ViewModel()
        {
            var currentTime = DateTime.Now;
            var gameStatuses = Enumerable.Empty<GameStatus>();
            GameServiceMock.Setup(service => service.GetNonCompletedGamesStatus(CustomUserIdentity.BranchName, currentTime))
                                 .Returns(gameStatuses);

            var viewResult = GameController.Index(CustomUserIdentity);

            viewResult.Model.ShouldEqual(gameStatuses);
        }
    }
}