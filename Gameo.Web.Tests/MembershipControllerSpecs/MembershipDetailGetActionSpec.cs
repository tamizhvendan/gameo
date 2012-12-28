using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class MembershipDetailGetActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void Returns_Membership_Detail_View_with_MembershipDetailRequest_as_View_model()
        {
            var viewResult = MembershipController.MembershipDetail();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldBeType<MembershipDetaiRequestViewModel>();
            viewResult.Model.ShouldNotBeNull();
        }
    }
}