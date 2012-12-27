using System;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class MembershipRechargeSpec : EntitySpecBase
    {
        private MembershipReCharge membershipReCharge;

        [SetUp]
        public void SetUp()
        {
            membershipReCharge = new MembershipReCharge {Price = 4, Hours = 1};
        }

        [Test]
        public void RechargedOn_should_be_current_time_by_default()
        {
            var dateTime = DateTime.Now;

            membershipReCharge.RechargedOn.Day.ShouldEqual(dateTime.Day);
            membershipReCharge.RechargedOn.Month.ShouldEqual(dateTime.Month);
            membershipReCharge.RechargedOn.Year.ShouldEqual(dateTime.Year);
        }

        [Test]
        [TestCase(0, false)]
        [TestCase(-10, false)]
        [TestCase(-1, false)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void Price_should_be_greater_than_zero(decimal price, bool isHappyPath)
        {
            membershipReCharge.Price = price;

            if (isHappyPath)
            {
                AssertZeroValidationError(membershipReCharge);
            }
            else
            {
                AssertEntityValidationError(membershipReCharge, "Price should be greater than zero.");
            }

        }

        [Test]
        [TestCase(0, false)]
        [TestCase(-10, false)]
        [TestCase(-1, false)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void Hours_should_be_greater_than_zero(int hours, bool isHappyPath)
        {
            membershipReCharge.Hours = hours;

            if (isHappyPath)
            {
                AssertZeroValidationError(membershipReCharge);
            }
            else
            {
                AssertEntityValidationError(membershipReCharge, "Hours should be greater than zero.");
            }

        }
    }
}