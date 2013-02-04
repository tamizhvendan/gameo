using System;
using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class AssignConsolePostActionSpec : MemebershipControllerSpecBase
    {
        private MembershipAssignConsoleViewModel membershipAssignConsoleViewModel;

        [SetUp]
        public void SetUp()
        {
            membershipAssignConsoleViewModel = new MembershipAssignConsoleViewModel { Game = { BranchName = CustomUserIdentity.BranchName } };
        }

        [Test]
        public void If_model_state_is_invalid_return_Assign_Console_View()
        {
            SetUpRepositoryWithGamingConsoles();
            MembershipController.ModelState.AddModelError("foo", "bar");

            var viewResult = MembershipController.AssignConsole(membershipAssignConsoleViewModel) as ViewResult;

            AssertGamingConsolesInViewBag(viewResult);
            viewResult.Model.ShouldEqual(membershipAssignConsoleViewModel);
            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void If_game_hours_is_greater_than_membership_remaining_hours_return_Assign_Console_View_with_model_error()
        {
            var dateTime = DateTime.UtcNow.ToIST();
            membershipAssignConsoleViewModel.Game.InTime = dateTime;
            membershipAssignConsoleViewModel.Game.OutTime = dateTime.AddHours(1.5);
            var membership = new Membership();
            membership.Recharge(new MembershipReCharge { Hours = 1});
            SetUpRepositoryWithGamingConsoles();
            MembershipRepositoryMock
                .Setup(repo => repo.FindByMembershipId(membershipAssignConsoleViewModel.Membership.MembershipId))
                .Returns(membership);

            var viewResult = MembershipController.AssignConsole(membershipAssignConsoleViewModel) as ViewResult;

            AssertGamingConsolesInViewBag(viewResult);
            AssertModelError(MembershipController, "Game", "Membership has only " + membership.RemainingHours + " hours. Please recharge!");
            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void If_model_state_is_valid_assign_console_using_game_service_and_redirect_to_game_index()
        {
            var dateTime = DateTime.UtcNow.ToIST();
            membershipAssignConsoleViewModel.Game.InTime = dateTime;
            membershipAssignConsoleViewModel.Game.OutTime = dateTime.AddHours(1.5);
            var membership = new Membership();
            membership.Recharge(new MembershipReCharge { Hours = 2 });
            MembershipRepositoryMock
                .Setup(repo => repo.FindByMembershipId(membershipAssignConsoleViewModel.Membership.MembershipId))
                .Returns(membership);
            GameServiceMock.Setup(service => service.AssignConsoleForMembership(membership, membershipAssignConsoleViewModel.Game)).Verifiable();

            var actionResult = MembershipController.AssignConsole(membershipAssignConsoleViewModel);

            GameServiceMock.Verify(service => service.AssignConsoleForMembership(membership, membershipAssignConsoleViewModel.Game));
            AssertReadirectToAction(actionResult, "Index", "Game");
            MembershipController.TempData["Message"].ShouldEqual("Game assigned to membership successfully");
        }
    }
}