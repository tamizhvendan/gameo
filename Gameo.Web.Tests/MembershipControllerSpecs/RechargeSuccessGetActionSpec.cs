using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class RechargeSuccessGetActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void Returns_Recharge_Success_View_with_MembershipId_in_ViewBag()
        {
            var membershipId = "foo";
            MembershipController.TempData["MembershipId"] = membershipId;

            var viewResult = MembershipController.RechargeSuccess();

            viewResult.ViewName.ShouldEqual(string.Empty);
            var actualMembershipId = viewResult.ViewBag.MembershipId as string;
            actualMembershipId.ShouldEqual(membershipId);
        }
    }
}