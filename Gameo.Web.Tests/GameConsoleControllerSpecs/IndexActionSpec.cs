using System.Linq;
using Gameo.Domain;
using Gameo.Web.Tests.BranchControllerSpecs;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    [TestFixture]
    public class IndexActionSpec : GameConsoleControllerSpecBase
    {
        [Test]
        public void Retrieve_all_GameConsoles_from_GameConsoles_repository()
        {
            GameConsoleRepositoryMock.Setup(repo => repo.All).Verifiable();

            GameConsoleController.Index();

            GameConsoleRepositoryMock.Verify(repo => repo.All, Times.Once());
        }

        [Test]
        public void Returns_index_view_with_list_of_gameconsoles()
        {
            var gameConsoles = Enumerable.Empty<GameConsole>();
            GameConsoleRepositoryMock.Setup(repo => repo.All).Returns(gameConsoles);

            var viewResult = GameConsoleController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(gameConsoles);
        }
    }
}