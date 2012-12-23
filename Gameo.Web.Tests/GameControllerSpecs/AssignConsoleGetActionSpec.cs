using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
            User.BranchName = "Branch1";
            var gamingConsoles = new List<GamingConsole>
                                     {
                                         new GamingConsole
                                             {
                                                 Name = "Console1",
                                             }
                                     };
            GamingConsoleRepositoryMock.Setup(repo => repo.GetGamingConsolesByBranchName(User.BranchName))
                                        .Returns(gamingConsoles);

            var viewResult = GameController.AssignConsole(CustomUserIdentity);

            var selectListItems = viewResult.ViewBag.GamingConsoles as IEnumerable<SelectListItem>;
            selectListItems.Count().ShouldEqual(1);
            selectListItems.First().Text.ShouldEqual("Console1");
            selectListItems.First().Value.ShouldEqual("Console1");
        }

        [Test]
        public void Returns_AssignConsole_View_with_ViewModel_as_List_of_6_Game_entities()
        {
            var viewResult = GameController.AssignConsole(CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
            var games = viewResult.Model as List<Game>;

            games.Count.ShouldEqual(6);
        }
    }
}