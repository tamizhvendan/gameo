using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Controllers;
using Gameo.Web.Models;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    public abstract class GameControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IGamingConsoleRepository> GamingConsoleRepositoryMock;
        protected GameController GameController;
        protected CustomUserIdentity CustomUserIdentity;
        protected User User;

        [SetUp]
        public void GameControllerSpecSetUp()
        {
            GamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            GameController = new GameController(GamingConsoleRepositoryMock.Object);
            User = new User();
            CustomUserIdentity = new CustomUserIdentity(User);
        }
    }
}