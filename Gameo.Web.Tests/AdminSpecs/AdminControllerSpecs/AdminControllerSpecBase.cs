using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.AdminControllerSpecs
{
    public abstract class AdminControllerSpecBase : ControllerSpecBase
    {
        protected AdminController AdminController;
        protected Mock<IGameRepository> GameRepositoryMock;
        protected Mock<IGameService> GameServiceMock;


        [SetUp]
        public void AdminControllerSpecBaseSetUp()
        {
            GameRepositoryMock = new Mock<IGameRepository>();
            GameServiceMock = new Mock<IGameService>();
            AdminController = new AdminController(BranchRepositoryMock.Object, GameServiceMock.Object, GameRepositoryMock.Object);
        }
    }
}