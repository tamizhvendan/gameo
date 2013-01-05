using System;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class CompletedGamesGetActionSpec : GameControllerSpecBase
    {
        [Test]
        public void Retrieves_completed_games_from_game_repo_and_pass_it_to_view()
        {
            var dateTime = DateTime.Now;
            GameRepositoryMock.Setup(repo => repo.GetCompletedGamesWithinGivenDay(CustomUserIdentity.BranchName, dateTime))
                                .Returns(Games);

            var viewResult = GameController.CompletedGames(CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);  
            viewResult.Model.ShouldEqual(Games);
        }
    }
}