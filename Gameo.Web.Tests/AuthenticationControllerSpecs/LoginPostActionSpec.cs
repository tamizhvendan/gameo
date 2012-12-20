using System;
using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AuthenticationControllerSpecs
{
    [TestFixture]
    public class LoginPostActionSpec : AuthenticationControllerSpecBase
    {
        private LoginViewModel loginViewModel;

        [SetUp]
        public void SetUp()
        {
            loginViewModel = new LoginViewModel { BranchName = "", Password = "", UserName = ""};
        }

        [Test]
        public void If_user_credentials_not_valid_render_login_view_with_model_error()
        {
            var argumentException = new ArgumentException("foo");
            SetupBranchRepositoryToReturnSomeRandomBranches();
            AuthenticationServiceMock
                .Setup(service => service.Authenticate(loginViewModel.UserName, loginViewModel.Password, loginViewModel.BranchName))
                .Throws(argumentException);

            var viewResult = AuthenticationController.Login(loginViewModel) as ViewResult;

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(loginViewModel);
            AssertModelError(AuthenticationController, "UserName", argumentException.Message);
            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void SetAuthCookie_upon_successful_authentication()
        {
            AuthenticationServiceMock.Setup(service => service.SetAuthCookie(loginViewModel.UserName)).Verifiable();

            AuthenticationController.Login(loginViewModel);

            AuthenticationServiceMock.Verify(service => service.SetAuthCookie(loginViewModel.UserName));
        }

        [Test]
        public void Redirect_to_Admin_Dashboard_if_logged_user_is_admin()
        {
            AuthenticationServiceMock
                .Setup(service => service.Authenticate(loginViewModel.UserName, loginViewModel.Password, loginViewModel.BranchName))
                .Returns(new User{ IsAdmin = true });

            var actionResult = AuthenticationController.Login(loginViewModel) as RedirectToRouteResult;
            
            AssertReadirectToAction(actionResult, "Index", "Home", "Admin");
        }

        [Test]
        public void Redirect_to_Game_Dashboard_if_logged_user_is_non_admin()
        {
            AuthenticationServiceMock
                .Setup(service => service.Authenticate(loginViewModel.UserName, loginViewModel.Password, loginViewModel.BranchName))
                .Returns(new User());

            var actionResult = AuthenticationController.Login(loginViewModel) as RedirectToRouteResult;

            AssertReadirectToAction(actionResult, "Index", "Game");
        }
    }
}