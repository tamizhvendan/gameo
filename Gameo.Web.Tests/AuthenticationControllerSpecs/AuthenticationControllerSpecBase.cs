using Gameo.Services;
using Gameo.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AuthenticationControllerSpecs
{
    public abstract class AuthenticationControllerSpecBase : ControllerSpecBase
    {
        protected AuthenticationController AuthenticationController;
        protected Mock<IAuthenticationService> AuthenticationServiceMock;

        [SetUp]
        public void SetUp()
        {
            AuthenticationServiceMock = new Mock<IAuthenticationService>();
            AuthenticationController = new AuthenticationController(BranchRepositoryMock.Object, AuthenticationServiceMock.Object);
        }
    }
}