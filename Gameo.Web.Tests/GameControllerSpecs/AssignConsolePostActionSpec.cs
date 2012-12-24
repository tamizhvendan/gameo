using System.Web.Mvc;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class AssignConsolePostActionSpec : GameControllerSpecBase
    {
        [Test]
        public void If_Model_state_is_invalid_Retrieves_GamingConsoles_of_user_branch_from_GamingConsole_Repository_and_put_them_in_ViewBag()
        {
            GameController.ModelState.AddModelError("foo", "bar");
            SetUpRepositoryWithGamingConsoles();

            var viewResult = GameController.AssignConsole(games, CustomUserIdentity) as ViewResult;

            AssertGamingConsolesInViewBag(viewResult);
        }

        [Test]
        public void If_Model_state_is_invalid_returns_AssignConsole_View_with_same_ViewModel()
        {
            GameController.ModelState.AddModelError("foo", "bar");

            var viewResult = GameController.AssignConsole(games, CustomUserIdentity) as ViewResult;

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(games);
        }

        [Test]
        public void If_Model_state_is_Valid_Add_Games_using_Game_Repository()
        {
            GameRepositoryMock.Setup(repo => repo.AddMany(games)).Verifiable();

            GameController.AssignConsole(games, CustomUserIdentity);

            GameRepositoryMock.Verify(repo => repo.AddMany(games));
        }

        [Test]
        public void After_adds_the_game_in_repository_redirects_to_index_action()
        {
            var actionResult = GameController.AssignConsole(games, CustomUserIdentity);
            AssertReadirectToIndexAction(actionResult);
        }
    }
}