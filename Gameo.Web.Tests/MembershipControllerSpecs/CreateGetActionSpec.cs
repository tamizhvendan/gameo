using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : MemebershipControllerSpecBase
    {
        [Test]
        public void Returns_Create_View_having_Membership_with_BranchName_updated_as_view_model()
        {
            var viewResult = MembershipController.Create(CustomUserIdentity);

            viewResult.ViewName.ShouldEqual(string.Empty);
            var membership = viewResult.Model as Membership;
            membership.BranchName.ShouldEqual(CustomUserIdentity.BranchName);
        }
    }
}