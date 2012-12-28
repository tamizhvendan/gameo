using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class MembershipRepositorySpec : RepositorySpecBase<Membership>
    {
        private MembershipRepository membershipRepository;

        [SetUp]
        public void SetUp()
        {
            membershipRepository = new MembershipRepository();
        }

        [Test]
        public void IsCustomer1ContactNumberExists_returns_true_if_contact_number_of_customer1_exists()
        {
            var membership = new Membership {Customer1ContactNumber = "9988"};
            AddEntityToDatabase(membership);

            membershipRepository.IsCustomer1ContactNumberExists("9988").ShouldBeTrue();
        }

        [Test]
        public void IsCustomer1ContactNumberExists_returns_false_if_contact_number_of_customer1__not_exists()
        {
            var membership = new Membership { Customer1ContactNumber = "99889" };
            AddEntityToDatabase(membership);

            membershipRepository.IsCustomer1ContactNumberExists("9988").ShouldBeFalse();
        }

        [Test]
        public void FindByMembershipId_return_Membership_if_membershipid_exists()
        {
            var membership = new Membership { Customer1ContactNumber = "99889" };
            AddEntityToDatabase(membership);

            var actualMembership = membershipRepository.FindByMembershipId(membership.MembershipId);

            actualMembership.Customer1ContactNumber.ShouldEqual(membership.Customer1ContactNumber);
        }

        [Test]
        public void FindByMembershipId_return_null_if_membershipid_not_exists()
        {
            var membership = new Membership { Customer1ContactNumber = "99889" };
            AddEntityToDatabase(membership);

            var actualMembership = membershipRepository.FindByMembershipId("foo");

            actualMembership.ShouldBeNull();
        }

        [Test]
        public void FindByCustomer1ContactNumber_return_Membership_if_contactnumber_of_customer1_exists()
        {
            var membership = new Membership { Customer1ContactNumber = "99889" };
            AddEntityToDatabase(membership);

            var actualMembership = membershipRepository.FindByCustomer1ContactNumber(membership.Customer1ContactNumber);

            actualMembership.Customer1ContactNumber.ShouldEqual(membership.Customer1ContactNumber);
        }

        [Test]
        public void FindByCustomer1ContactNumber_return_null_if_contactnumber_of_customer1_not_exists()
        {
            var membership = new Membership { Customer1ContactNumber = "99889" };
            AddEntityToDatabase(membership);

            var actualMembership = membershipRepository.FindByCustomer1ContactNumber("foo");

            actualMembership.ShouldBeNull();
        }
    }
}