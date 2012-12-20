using System;
using System.Web.Mvc;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AuthenticationControllerSpecs
{
    [TestFixture]
    public class LoginPostActionSpec : AuthenticationControllerSpecBase
    {
        [Test]
        public void If_user_credentials_not_valid_render_login_view_with_model_error()
        {
            var loginViewModel = new LoginViewModel();
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
        public void Redirect_to_Game_Dashboard_if_logged_in_user_is_non_admin()
        {
            
        }
    }
}