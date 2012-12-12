using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GamingConsoleControllerSpecs
{
    [TestFixture]
    public class EditGetActionSpec : GamingConsoleControllerSpecBase
    {
        private Guid guid;

        [SetUp]
        public void SetUp()
        {
            guid = new Guid();
        }

        [Test]
        public void Retrives_the_GamingConsole_from_Repository_by_Id()
        {
            GamingConsoleRepositoryMock.Setup(repo => repo.GetById(guid)).Verifiable();

            GamingConsoleController.Edit(guid);

            GamingConsoleRepositoryMock.Verify(repo => repo.GetById(guid));
        }

        [Test]
        public void Returns_Edit_view_with_GamingConsole_retireved_from_repository()
        {
            var gamingConsole = new GamingConsole();
            GamingConsoleRepositoryMock.Setup(repo => repo.GetById(guid)).Returns(gamingConsole);

            var viewResult = GamingConsoleController.Edit(guid);

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(gamingConsole);
        }
    }
}