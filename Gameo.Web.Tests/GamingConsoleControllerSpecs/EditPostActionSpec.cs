using Gameo.Domain;
using NUnit.Framework;

namespace Gameo.Web.Tests.GamingConsoleControllerSpecs
{
    [TestFixture]
    public class EditPostActionSpec : GamingConsoleControllerSpecBase
    {
        private GamingConsole gamingConsole;

        [SetUp]
        public void SetUp()
        {
            gamingConsole = new GamingConsole();
        }

        [Test]
        public void Updates_the_GamingConsole_using_GamingConsole_repository()
        {
            GamingConsoleRepositoryMock.Setup(repo => repo.Update(gamingConsole)).Verifiable();

            GamingConsoleController.Edit(gamingConsole);

            GamingConsoleRepositoryMock.Verify(repo => repo.Update(gamingConsole));
        }

        [Test]
        public void Upon_successful_update_redirects_to_Index_page()
        {
            var actionResult = GamingConsoleController.Edit(gamingConsole);

            AssertReadirectToIndexAction(actionResult);
        }
    }
}