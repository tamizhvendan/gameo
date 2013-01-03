using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class MembershipGetActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void Return_Membership_View()
        {
            var viewResult = MembershipController.Membership();

            viewResult.ViewName.ShouldEqual(string.Empty);
        }
    }
}