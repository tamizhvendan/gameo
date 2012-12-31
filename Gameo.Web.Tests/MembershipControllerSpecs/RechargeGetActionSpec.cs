using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class RechargeGetActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void Returns_Recharge_View_With_Membership_Recharge_View_Model_with_branch_name_updated()
        {
            var viewResult = MembershipController.Recharge(CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
            var membershipReCharge = viewResult.Model as MembershipReCharge;
            membershipReCharge.BranchName.ShouldEqual(CustomUserIdentity.BranchName);
        }
    }
}