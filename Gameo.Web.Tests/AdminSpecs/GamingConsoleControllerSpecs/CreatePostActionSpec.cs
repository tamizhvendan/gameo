using System.Web.Mvc;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.GamingConsoleControllerSpecs
{
    public class CreatePostActionSpec : GamingConsoleControllerSpecBase
    {
        [Test]
        public void Adds_new_GamingConsole_to_the_GamingConsole_repository()
        {
            var gameConsole = new GamingConsole();
            GamingConsoleRepositoryMock.Setup(repo => repo.Add(gameConsole)).Verifiable();

            GamingConsoleController.Create(gameConsole);

            GamingConsoleRepositoryMock.Verify(repo => repo.Add(gameConsole));
        }

        [Test]
        public void Upon_successful_adding_redirect_to_the_GamingConsole_index_page()
        {
            var actionResult = GamingConsoleController.Create(new GamingConsole());

            AssertReadirectToIndexAction(actionResult);
        }

        [Test]
        public void Returns_create_view_if_new_GamingConsole_is_invalid()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            GamingConsoleController.ModelState.AddModelError("Name", "The Name field is required");

            var gameConsole = new GamingConsole();

            var viewResult = GamingConsoleController.Create(gameConsole) as ViewResult;

            viewResult.Model.ShouldEqual(gameConsole);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void Returns_create_view_if_new_GamingConsole_name_already_exists()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            var gameConsole = new GamingConsole { Name = "foo", BranchName = "bar" };
            GamingConsoleRepositoryMock.Setup(repo => repo.IsConsoleNameExists("foo", "bar")).Returns(true);

            var viewResult = GamingConsoleController.Create(gameConsole) as ViewResult;

            viewResult.Model.ShouldEqual(gameConsole);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(GamingConsoleController, "Name", "Console Name already exists in the selected branch");
            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}