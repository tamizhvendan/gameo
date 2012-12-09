using System.Web.Mvc;
using Gameo.Domain;
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
    }
}