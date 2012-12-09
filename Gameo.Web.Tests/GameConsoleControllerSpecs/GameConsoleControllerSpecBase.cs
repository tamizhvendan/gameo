using Gameo.DataAccess.Core;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    public abstract class GameConsoleControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IGameConsoleRepository> GameConsoleRepositoryMock;
        protected Mock<IBranchRepository> BranchRepositoryMock;
        protected GameConsoleController GameConsoleController;

        [SetUp]
        public void BranchControllerSpecSetUp()
        {
            BranchRepositoryMock = new Mock<IBranchRepository>();
            GameConsoleRepositoryMock = new Mock<IGameConsoleRepository>();
            GameConsoleController = new GameConsoleController(GameConsoleRepositoryMock.Object, BranchRepositoryMock.Object);    
        }
    }
}