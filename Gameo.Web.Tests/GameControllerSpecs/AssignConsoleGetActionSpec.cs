using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class AssignConsoleGetActionSpec : GameControllerSpecBase
    {
        [Test]
        public void Retrieves_GamingConsoles_of_user_branch_from_GamingConsole_Repository_and_put_them_in_ViewBag()
        {
            SetUpRepositoryWithGamingConsoles();

            var viewResult = GameController.AssignConsole(CustomUserIdentity);

            AssertGamingConsolesInViewBag(viewResult);
        }

        [Test]
        public void Returns_AssignConsole_View_with_ViewModel_as_List_of_1_Game_entities()
        {
            var viewResult = GameController.AssignConsole(CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
            var games = viewResult.Model as List<Game>;

            games.First().BranchName.ShouldEqual(CustomUserIdentity.BranchName);
            games.Count.ShouldEqual(1);
        }
    }
}