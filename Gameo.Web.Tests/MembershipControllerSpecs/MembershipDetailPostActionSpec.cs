using Gameo.Domain;
using Gameo.Web.ViewModels;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class MembershipDetailPostActionSpec : MemebershipControllerSpecBase
    {
        private MembershipDetaiRequestViewModel membershipDetaiRequestViewModel;
        private Membership membership;

        [SetUp]
        public void SetUp()
        {
            membershipDetaiRequestViewModel = new MembershipDetaiRequestViewModel();
            membership = new Membership();
        }

        [Test]
        public void Retrieves_Membership_using_FindByMembershipId_if_the_search_using_membershipid()
        {
            var membershipId = "foo";
            membershipDetaiRequestViewModel.MembershipId = membershipId;
            MembershipRepositoryMock.Setup(repo => repo.FindByMembershipId(membershipId)).Returns(membership);

            var viewResult = MembershipController.MembershipDetail(membershipDetaiRequestViewModel);

            var actualMembershipDetailRequest = viewResult.Model as MembershipDetaiRequestViewModel;
            viewResult.ViewName.ShouldEqual(string.Empty);
            actualMembershipDetailRequest.Membership.ShouldEqual(membership);
        }

        [Test]
        public void Retrieves_Membership_using_FindByCustomer1ContactNumber_if_the_search_using_contact_number_of_cutomer_1()
        {
            var contactNumber = "foo";
            membershipDetaiRequestViewModel.Customer1ContactNumber = contactNumber;
            MembershipRepositoryMock.Setup(repo => repo.FindByCustomer1ContactNumber(contactNumber)).Returns(membership);

            var viewResult = MembershipController.MembershipDetail(membershipDetaiRequestViewModel);

            var actualMembershipDetailRequest = viewResult.Model as MembershipDetaiRequestViewModel;
            viewResult.ViewName.ShouldEqual(string.Empty);
            actualMembershipDetailRequest.Membership.ShouldEqual(membership);
        }
    }
}