using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    public abstract class MemebershipControllerSpecBase : ControllerSpecBase
    {
        protected MembershipController MembershipController;
        protected Mock<IMembershipRepository> MembershipRepositoryMock;
        protected Mock<IGameService> GameServiceMock;

        [SetUp]
        public void MemebershipControllerSpecSetUp()
        {
            MembershipRepositoryMock = new Mock<IMembershipRepository>();
            GamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            GameServiceMock = new Mock<IGameService>();
            MembershipController = new MembershipController(MembershipRepositoryMock.Object, GamingConsoleRepositoryMock.Object, GameServiceMock.Object);
        }
    }
}
