using System.Web.Mvc;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class RechargePostActionSpec : MemebershipControllerSpecBase
    {
        private MembershipReCharge membershipReCharge;
        private const string MembershipId = "00213-32423432";

        [SetUp]
        public void SetUp()
        {
            membershipReCharge = new MembershipReCharge { MembershipId = MembershipId};
        }

        [Test]
        public void Return_Recharge_View_if_model_state_is_invalid()
        {
            MembershipController.ModelState.AddModelError("foo", "bar");

            var viewResult = MembershipController.Recharge(membershipReCharge) as ViewResult;

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(membershipReCharge);
        }

        [Test]
        public void Return_Recharge_View_with_model_error_if_membership_id_not_exists()
        {
            Membership membership = null;
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(MembershipId)).Returns(membership);

            var viewResult = MembershipController.Recharge(membershipReCharge) as ViewResult;

            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(MembershipController, "membershipId", "Membership Id not exists.");
            viewResult.Model.ShouldEqual(membershipReCharge);
        }

        [Test]
        public void Recharge_using_repository_if_model_State_is_valid()
        {
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(MembershipId)).Returns(new Membership());
            MembershipRepositoryMock.Setup(repo => repo.Recharge(membershipReCharge)).Verifiable();

            MembershipController.Recharge(membershipReCharge);

            MembershipRepositoryMock.Verify(repo => repo.Recharge(membershipReCharge));
        }

        [Test]
        public void Redirect_to_Recharge_Success_upon_successful_recharge()
        {
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(MembershipId)).Returns(new Membership());

            var actionResult = MembershipController.Recharge(membershipReCharge);

            AssertReadirectToAction(actionResult, "RechargeSuccess");
            var membershipId = MembershipController.TempData["MembershipId"] as string;
            membershipId.ShouldEqual(MembershipId);
        }

    }
}