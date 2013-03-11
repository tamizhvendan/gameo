using System;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class MembershipRepositorySpec : RepositorySpecBase<Membership>
    {
        private MembershipRepository membershipRepository;
        private Membership membership;
        private MembershipReCharge membershipReCharge;

        [SetUp]
        public void SetUp()
        {
            membershipRepository = new MembershipRepository();
            membership = new Membership { Customer1ContactNumber = "99889" };
            membershipReCharge = new MembershipReCharge { BranchName = "foo", Hours = 5, Price = 50, MembershipId = membership.MembershipId};
            AddEntityToDatabase(membership);

        }

        [Test]
        public void IsCustomer1ContactNumberExists_returns_true_if_contact_number_of_customer1_exists()
        {
            membershipRepository.IsCustomer1ContactNumberExists("99889").ShouldBeTrue();
        }

        [Test]
        public void IsCustomer1ContactNumberExists_returns_false_if_contact_number_of_customer1__not_exists()
        {
            membershipRepository.IsCustomer1ContactNumberExists("9988").ShouldBeFalse();
        }

        [Test]
        public void FindByMembershipId_return_Membership_if_membershipid_exists()
        {
            var actualMembership = membershipRepository.FindByMembershipId(membership.MembershipId);

            actualMembership.Customer1ContactNumber.ShouldEqual(membership.Customer1ContactNumber);
        }

        [Test]
        public void FindByMembershipId_return_null_if_membershipid_not_exists()
        {
            var actualMembership = membershipRepository.FindByMembershipId("foo");

            actualMembership.ShouldBeNull();
        }



        [Test]
        public void FindByCustomer1ContactNumber_return_Membership_if_contactnumber_of_customer1_exists()
        {
            var actualMembership = membershipRepository.FindByCustomer1ContactNumber(membership.Customer1ContactNumber);

            actualMembership.Customer1ContactNumber.ShouldEqual(membership.Customer1ContactNumber);
        }

        [Test]
        public void FindByCustomer1ContactNumber_return_null_if_contactnumber_of_customer1_not_exists()
        {
            var actualMembership = membershipRepository.FindByCustomer1ContactNumber("foo");

            actualMembership.ShouldBeNull();
        }

        [Test]
        public void Recharges_by_retrieving_the_membership_and_adding_the_recharge_to_it()
        {
            membershipRepository.Recharge(membershipReCharge);

            AssertUpdatedEntity(membership.Id, actualMembership =>
                                                   {
                                                       actualMembership.RemainingHours.ShouldEqual(5);
                                                       var reCharge = actualMembership.ReCharges.First();
                                                       reCharge.BranchName.ShouldEqual(membershipReCharge.BranchName);
                                                       reCharge.Hours.ShouldEqual(membershipReCharge.Hours);
                                                       reCharge.Price.ShouldEqual(membershipReCharge.Price);
                                                       reCharge.MembershipId.ShouldEqual(membership.MembershipId);
                                                   });
        }

        [Test]
        public void GetRecharges_Returns_the_recharges_for_given_day_and_given_branch_name()
        {
            var membership2 = new Membership {Customer1ContactNumber = "626262"};
            var rechargedOn = DateTime.UtcNow.ToIST().Subtract(new TimeSpan(1, 0, 0, 0));
            var reCharge = new MembershipReCharge
                               {
                                   BranchName = "foo", Hours = 2, Price = 20, RechargedOn = rechargedOn, MembershipId = membership2.MembershipId
                               };
            AddEntityToDatabase(membership2);
            membershipRepository.Recharge(membershipReCharge);
            membershipRepository.Recharge(reCharge);
            membershipRepository.Recharge(membershipReCharge);
            membershipRepository.Recharge(reCharge);

            var membershipReCharges = membershipRepository.GetRecharges("foo", DateTime.UtcNow.ToIST());
            membershipReCharges.Count().ShouldEqual(2);
            membershipReCharges.Sum(m => m.Hours).ShouldEqual(10);
            membershipReCharges.Sum(m => m.Price).ShouldEqual(100);
        }
    }
}