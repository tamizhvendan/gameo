using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class AddCustomerPostActionSpec : GameControllerSpecBase
    {
        [Test]
        public void Retrieves_GamingConsoles_of_user_branch_from_GamingConsole_Repository_and_put_them_in_ViewBag()
        {
            SetUpRepositoryWithGamingConsoles();

            var viewResult = GameController.AddCustomer(Games, CustomUserIdentity);

            AssertGamingConsolesInViewBag(viewResult);
        }

        [Test]
        public void Adds_one_more_game_instance_in_the_games_list_pass_it_as_view_model()
        {
            var viewResult = GameController.AddCustomer(Games, CustomUserIdentity);

            var actualGames = viewResult.Model as List<Game>;
            actualGames.Count.ShouldEqual(1);
            actualGames.First().BranchName.ShouldEqual(CustomUserIdentity.BranchName);
        }

        [Test]
        public void Returns_Assign_Console_View()
        {
            var viewResult = GameController.AddCustomer(Games, CustomUserIdentity);

            viewResult.ViewName.ShouldEqual("AssignConsole");
        }

        [Test]
        public void If_Model_state_is_invalid_returns_AssignConsole_View_with_same_ViewModel()
        {
            GameController.ModelState.AddModelError("foo", "bar");

            var viewResult = GameController.AddCustomer(Games, CustomUserIdentity);

            viewResult.ViewName.ShouldEqual("AssignConsole");
            viewResult.Model.ShouldEqual(Games);
        }
    }
}