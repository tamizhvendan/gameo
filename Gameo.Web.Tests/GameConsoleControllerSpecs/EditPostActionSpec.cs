using Gameo.Domain;
using NUnit.Framework;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    [TestFixture]
    public class EditPostActionSpec : GameConsoleControllerSpecBase
    {
        private GameConsole gameConsole;

        [SetUp]
        public void SetUp()
        {
            gameConsole = new GameConsole();
        }

        [Test]
        public void Updates_the_game_console_using_repository()
        {
            GameConsoleRepositoryMock.Setup(repo => repo.Update(gameConsole)).Verifiable();

            GameConsoleController.Edit(gameConsole);

            GameConsoleRepositoryMock.Verify(repo => repo.Update(gameConsole));
        }

        [Test]
        public void Upon_successful_update_redirects_to_Index_page()
        {
            var actionResult = GameConsoleController.Edit(gameConsole);

            AssertReadirectToIndexAction(actionResult);
        }
    }
}