using NUnit.Framework;

namespace Gameo.Web.Tests.AuthenticationControllerSpecs
{
    [TestFixture]
    public class LogOffActionSpec : AuthenticationControllerSpecBase
    {
        [Test]
        public void LogOff_using_authenticationService()
        {
            AuthenticationServiceMock.Setup(service => service.LogOff()).Verifiable();

            AuthenticationController.LogOff();

            AuthenticationServiceMock.Verify(service => service.LogOff());
        }

        [Test]
        public void Redirect_to_login_page_after_logout()
        {
            var redirectToRouteResult = AuthenticationController.LogOff();

            AssertReadirectToAction(redirectToRouteResult, "Login");
        }
    }
}