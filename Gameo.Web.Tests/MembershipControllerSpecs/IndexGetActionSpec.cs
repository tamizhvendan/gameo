using NUnit.Framework;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void Redirects_to_Membership_Detail_Action()
        {
            var actionResult = MembershipController.Index();

            AssertReadirectToAction(actionResult, "MembershipDetail");
        }
    }
}