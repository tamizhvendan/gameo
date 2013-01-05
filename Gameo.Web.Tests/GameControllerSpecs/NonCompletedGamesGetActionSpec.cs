using System;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class NonCompletedGamesGetActionSpec : GameControllerSpecBase
    {
        [Test]
        public void Retrieves_non_completed_games_from_game_repo_and_pass_as_view_model()
        {
            var currentTime = DateTime.Now;
            GameRepositoryMock.Setup(repo => repo.GetNonCompletedGames(CustomUserIdentity.BranchName, currentTime))
                                .Returns(Games);

            var viewResult = GameController.NonCompletedGames(CustomUserIdentity);

            viewResult.Model.ShouldEqual(Games);
            viewResult.ViewName.ShouldEqual(string.Empty);
        }
    }
}