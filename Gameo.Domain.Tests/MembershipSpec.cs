﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class MembershipSpec : EntitySpecBase
    {
        private Membership membership;

        [SetUp]
        public void SetUp()
        {
            membership = new Membership {Customer1Name = "foo", Customer1ContactNumber = "9999988888"};
        }

        [Test]
        public void Customer1Name_is_required()
        {
            membership.Customer1Name = null;

            AssertEntityValidationError(membership, "Customer Name is required.");
        }

        [Test]
        public void Customer1ContactNumber_is_required()
        {
            membership.Customer1ContactNumber = null;

            AssertEntityValidationError(membership, "Customer Contact Number is required.");
        }

        [Test]
        public void IssuedOn_should_be_current_time_by_default()
        {
            var dateTime = DateTime.Now;
            membership.IssuedOn.Day.ShouldEqual(dateTime.Day);
            membership.IssuedOn.Month.ShouldEqual(dateTime.Month);
            membership.IssuedOn.Year.ShouldEqual(dateTime.Year);
        }

        [Test]
        public void MembershipId_is_combination_of_date_of_issue_and_customer_contact_number()
        {
            var issuedOn = membership.IssuedOn;
            var expectedMembershipId = string.Format("{0}{1}{2}-{3}",issuedOn.Year, issuedOn.Month, issuedOn.Day, membership.Customer1ContactNumber);

            membership.MembershipId.ShouldEqual(expectedMembershipId);
        }

        [Test]
        public void ExpiresOn_should_be_6_months_after_the_last_membership_recharge_date()
        {
            var dateTime = DateTime.Now;
            var membershipReCharges = new List<MembershipReCharge>
                                          {
                                              new MembershipReCharge { RechargedOn = dateTime.Subtract(new TimeSpan(45,0,0,0))},
                                              new MembershipReCharge { RechargedOn = dateTime},
                                              new MembershipReCharge { RechargedOn = dateTime.AddMonths(1) },
                                              new MembershipReCharge { RechargedOn = dateTime.AddMonths(2)}
                                          };
            membershipReCharges.ForEach(membershipReCharge => membership.Recharge(membershipReCharge));

            membership.ExpiresOn.ShouldEqual(dateTime.AddMonths(8));
        }

        [Test]
        public void IsExpired_should_be_true_if_ExpiresOn_is_less_than_current_day()
        {
            membership.Recharge(new MembershipReCharge { RechargedOn = DateTime.Now.Subtract(new TimeSpan(230,0,0,0))});

            membership.IsExpired.ShouldBeTrue();    
        }

        [Test]
        public void IsExpired_should_be_false_if_ExpiresOn_is_greater_than_or_equal_to_current_day()
        {
            membership.Recharge(new MembershipReCharge { RechargedOn = DateTime.Now });
            membership.IsExpired.ShouldBeFalse();

            membership.Recharge(new MembershipReCharge { RechargedOn = DateTime.Now.Subtract(new TimeSpan(1,0,0,0))});
            membership.IsExpired.ShouldBeFalse();
        }

        [Test]
        public void IsExpired_should_be_true_if_no_recharge_available()
        {
            membership.IsExpired.ShouldBeTrue();
        }

        [Test]
        public void Recharges_with_new_membership_recharge()
        {
            var membershipReCharge = new MembershipReCharge();
            
            membership.Recharge(membershipReCharge);

            membership.ReCharges.Count().ShouldEqual(1);
            membership.ReCharges.First().ShouldEqual(membershipReCharge);
        }

        [Test]
        public void Adds_new_game()
        {
            var game = new Game();

            membership.AddGame(game);

            membership.Games.Count().ShouldEqual(1);
            membership.Games.First().ShouldEqual(game);
        }

        [Test]
        public void Payment_type_changed_to_membership_when_adding_new_game()
        {
            var game = new Game();

            membership.AddGame(game);

            game.GamePaymentType.ShouldEqual(GamePaymentType.Membership);
        }

        [Test]
        public void RemainingHours_is_the_difference_of_sum_of_recharge_hours_minus_sum_of_games_played_hours()
        {
            var game = new Game();
            var game2 = new Game { InTime = DateTime.Now.AddMinutes(30)};

            var recharge = new MembershipReCharge { Hours = 4 };

            membership.Recharge(recharge);
            membership.AddGame(game);
            membership.AddGame(game2);

            membership.RemainingHours.ShouldEqual(2.5);
        }
    }
}