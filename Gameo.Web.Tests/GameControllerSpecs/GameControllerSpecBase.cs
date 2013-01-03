using System.Collections.Generic;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    public abstract class GameControllerSpecBase : ControllerSpecBase
    {
        protected GameController GameController;
        
        protected List<Game> Games;
        protected Mock<IGameRepository> GameRepositoryMock;
        protected Mock<IGameService> GameServiceMock;

        [SetUp]
        public void GameControllerSpecSetUp()
        {
            GamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            GameRepositoryMock = new Mock<IGameRepository>();
            GameServiceMock = new Mock<IGameService>();
            GameController = new GameController(GameRepositoryMock.Object, GamingConsoleRepositoryMock.Object, GameServiceMock.Object);
            Games = new List<Game>();
        }
    }
}