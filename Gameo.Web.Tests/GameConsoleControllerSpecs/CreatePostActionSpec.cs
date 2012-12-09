using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.Areas.Admin.Controllers;
using Gameo.Web.Tests.BranchControllerSpecs;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    public class CreatePostActionSpec : GameConsoleControllerSpecBase
    {
        [Test]
        public void Adds_new_gameconsole_to_the_gameconsole_repository()
        {
            var gameConsole = new GameConsole();
            GameConsoleRepositoryMock.Setup(repo => repo.Add(gameConsole)).Verifiable();

            GameConsoleController.Create(gameConsole);

            GameConsoleRepositoryMock.Verify(repo => repo.Add(gameConsole));
        }

        [Test]
        public void Upon_successful_adding_redirect_to_the_GameConsole_index_page()
        {
            var actionResult = GameConsoleController.Create(new GameConsole());

            AssertReadirectToIndexAction(actionResult);
        }

        [Test]
        public void Returns_create_view_if_new_gameconsole_is_invalid()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            GameConsoleController.ModelState.AddModelError("Name", "The Name field is required");

            var gameConsole = new GameConsole();

            var viewResult = GameConsoleController.Create(gameConsole) as ViewResult;

            viewResult.Model.ShouldEqual(gameConsole);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void Returns_create_view_if_new_gameconsole_name_already_exists()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            var gameConsole = new GameConsole { Name = "foo", BranchName = "bar" };
            GameConsoleRepositoryMock.Setup(repo => repo.IsConsoleNameExists("foo", "bar")).Returns(true);

            var viewResult = GameConsoleController.Create(gameConsole) as ViewResult;

            viewResult.Model.ShouldEqual(gameConsole);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(GameConsoleController, "Name", "Console Name already exists in the selected branch");
            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}