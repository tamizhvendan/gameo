using System.Linq;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GamingConsoleControllerSpecs
{
    [TestFixture]
    public class IndexActionSpec : GamingConsoleControllerSpecBase
    {
        [Test]
        public void Retrieve_all_GamingConsoles_from_GamingConsoles_repository()
        {
            GamingConsoleRepositoryMock.Setup(repo => repo.All).Returns(Enumerable.Empty<GamingConsole>()).Verifiable();

            GamingConsoleController.Index();

            GamingConsoleRepositoryMock.Verify(repo => repo.All, Times.Once());
        }

        [Test]
        public void Returns_index_view_with_list_of_GamingConsoles()
        {
            var gameConsoles = Enumerable.Empty<GamingConsole>();
            GamingConsoleRepositoryMock.Setup(repo => repo.All).Returns(gameConsoles);

            var viewResult = GamingConsoleController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(gameConsoles);
        }
    }
}