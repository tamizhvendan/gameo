using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    [TestFixture]
    public class EditGetActionSpec : GameConsoleControllerSpecBase
    {
        private Guid guid;

        [SetUp]
        public void SetUp()
        {
            guid = new Guid();
        }

        [Test]
        public void Retrives_the_GameConsole_from_Repository_by_Id()
        {
            GameConsoleRepositoryMock.Setup(repo => repo.GetById(guid)).Verifiable();

            GameConsoleController.Edit(guid);

            GameConsoleRepositoryMock.Verify(repo => repo.GetById(guid));
        }

        [Test]
        public void Returns_Edit_view_with_GameConsole_retireved_from_repository()
        {
            var gameConsole = new GameConsole();
            GameConsoleRepositoryMock.Setup(repo => repo.GetById(guid)).Returns(gameConsole);

            var viewResult = GameConsoleController.Edit(guid);

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(gameConsole);
        }
    }
}