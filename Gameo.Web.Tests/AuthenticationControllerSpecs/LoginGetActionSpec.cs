using Gameo.Web.Areas.Admin.Controllers;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AuthenticationControllerSpecs
{
    [TestFixture]
    public class LoginGetActionSpec : AuthenticationControllerSpecBase
    {
        [Test]
        public void Returns_login_view_with_login_view_model()
        {
            var viewResult = AuthenticationController.Login();

            viewResult.ViewName.ShouldEqual(string.Empty);

            viewResult.Model.ShouldBeType<LoginViewModel>();
        }

        [Test]
        public void Retrieves_list_of_branches_and_pass_it_to_view()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = AuthenticationController.Login();

            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}