using Gameo.Domain;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class MembershipPostActionSpec  :MemebershipControllerSpecBase
    {
        private string membershipId;

        [SetUp]
        public void SetUp()
        {
            membershipId = "20130101-9953882323";
        }

        [Test]
        public void Returns_Membership_View_with_model_error_if_membership_id_not_exists()
        {
            Membership membership = null;
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(membershipId)).Returns(membership);

            var viewResult = MembershipController.Membership(membershipId, CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(MembershipController, "membershipId", "Membership Id doesn't exists.");
        }

        [Test]
        public void Returns_Membership_View_with_model_error_if_membership_is_expired()
        {
            var membership = new Membership();
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(membershipId)).Returns(membership);

            var viewResult = MembershipController.Membership(membershipId, CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(MembershipController, "membershipId", "Membership Id is expired. Kindly recharge.");
        }

        [Test]
        public void Returns_AssignConsole_view_with_membership_assign_console_ViewModel_if_membership_id_exists()
        {
            var membership = new Membership();
            membership.Recharge(new MembershipReCharge { Hours = 5 });
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(membershipId)).Returns(membership);

            var viewResult = MembershipController.Membership(membershipId, CustomUserIdentity);

            viewResult.ViewName.ShouldEqual("AssignConsole");
            var membershipAssignConsoleViewModel = viewResult.Model as MembershipAssignConsoleViewModel;
            membershipAssignConsoleViewModel.Game.ShouldNotBeNull();
            membershipAssignConsoleViewModel.Game.Price.ShouldEqual(0);
            membershipAssignConsoleViewModel.Game.GamePaymentType.ShouldEqual(GamePaymentType.Membership);
            membershipAssignConsoleViewModel.Game.BranchName.ShouldEqual(CustomUserIdentity.BranchName);
            membershipAssignConsoleViewModel.Membership.ShouldEqual(membership);
        }

        [Test]
        public void if_membership_id_exists_Retrieves_GamingConsoles_of_user_branch_from_repository_and_put_them_in_ViewBag()
        {
            var membership = new Membership();
            membership.Recharge(new MembershipReCharge { Hours = 5 });
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(membershipId)).Returns(membership);
            SetUpRepositoryWithGamingConsoles();

            var viewResult = MembershipController.Membership(membershipId, CustomUserIdentity);

            AssertGamingConsolesInViewBag(viewResult);
        }
    }
}