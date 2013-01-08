using System.Web.Mvc;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class AssignConsolePostActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void If_model_state_is_invalid_return_Assign_Console_View()
        {
            SetUpRepositoryWithGamingConsoles();
            var membershipAssignConsoleViewModel = new MembershipAssignConsoleViewModel
                                                       {Game = {BranchName = CustomUserIdentity.BranchName}};
            MembershipController.ModelState.AddModelError("foo", "bar");

            var viewResult = MembershipController.AssignConsole(membershipAssignConsoleViewModel) as ViewResult;

            AssertGamingConsolesInViewBag(viewResult);
            viewResult.Model.ShouldEqual(membershipAssignConsoleViewModel);
            viewResult.ViewName.ShouldEqual(string.Empty);
        }
    }
}