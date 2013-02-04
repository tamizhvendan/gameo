using System;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class GameSpec : EntitySpecBase
    {
        private Game game;

        [SetUp]
        public void SetUp()
        {
            game = new Game
                       {
                           ConsoleName = "Console1", CustomerName = "Customer1", Price = 10, BranchName = "Branch1"
                       };
        }

        [Test]
        public void CustomerName_is_required()
        {
            game.CustomerName = null;

            AssertEntityValidationError(game, "Customer Name is required.");
        }

        [Test]
        public void In_time_is_required()
        {
            game.InTime = DateTime.MinValue;

            AssertEntityValidationError(game, "In Time is required.");
        }

        [Test]
        public void ConsoleName_is_required()
        {
            game.ConsoleName = null;

            AssertEntityValidationError(game, "Console Name is required.");
        }

        [Test]
        public void Out_time_is_required()
        {
            game.OutTime = DateTime.MinValue;

            AssertEntityValidationError(game, "Out Time is required.");
        }

        [Test]
        public void Branch_Name_is_required()
        {
            game.BranchName = null;

            AssertEntityValidationError(game, "Branch Name is required.");
        }

        [Test]
        public void Out_time_should_be_greater_than_in_time()
        {
            game.InTime = DateTime.UtcNow.ToIST();
            game.OutTime = game.InTime.Subtract(new TimeSpan(0, 0, 30, 0));

            AssertEntityValidationError(game, "Out time should be greater than in time.");
            game.OutTime = game.InTime;
            AssertEntityValidationError(game, "Out time should be greater than in time.");
        }

        [Test]
        [TestCase(29, false)]
        [TestCase(30, true)]
        [TestCase(31, false)]
        [TestCase(59, false)]
        [TestCase(60, true)]
        [TestCase(61, false)]
        public void Difference_between_In_time_and_Out_time_should_be_in_multiples_of_30_minutes(int noOfMinutesToAdd, bool isHappyPath)
        {
            game.InTime = DateTime.UtcNow.ToIST();
            game.OutTime = DateTime.UtcNow.ToIST().AddMinutes(noOfMinutesToAdd);

            if (isHappyPath)
            {
                AssertZeroValidationError(game);
            }
            else
            {
                AssertEntityValidationError(game, "Difference between In Time and Out Time should be in multiples of half-hour.");    
            }
        }

        [Test]
        public void Validates_the_difference_in_seconds_of_InTime_and_OutTime()
        {
            game.InTime = DateTime.UtcNow.ToIST();
            game.OutTime = game.InTime.AddSeconds(5);

            AssertEntityValidationError(game, "Difference between In Time and Out Time should be in multiples of half-hour."); 
        }

        [Test]
        [TestCase(0, false)]
        [TestCase(10, true)]
        [TestCase(-4, false)]
        [TestCase(-1, false)]
        public void Price_should_be_greater_than_zero_if_the_payment_type_is_one_time(decimal price, bool isHappyPath = false)
        {
            game.GamePaymentType = GamePaymentType.OneTime;
            game.Price = price;

            if (isHappyPath)
            {
                AssertZeroValidationError(game);
            }
            else
            {
                AssertEntityValidationError(game, "Price should be greater than zero.");
            }
        }

        [Test]
        public void Price_can_be_zero_if_the_payment_type_is_membership()
        {
            game.GamePaymentType = GamePaymentType.Membership;
            game.Price = 0;

            AssertZeroValidationError(game);

        }

        [Test]
        public void By_default_InTime_should_be_current_time()
        {
            var currentTime = DateTime.UtcNow.ToIST();

            AssertDateTimeEquality(game.InTime, currentTime);
        }

        private void AssertDateTimeEquality(DateTime actualTime, DateTime expectedTime)
        {
            actualTime.Minute.ShouldEqual(expectedTime.Minute);
            actualTime.Hour.ShouldEqual(expectedTime.Hour);
            actualTime.Day.ShouldEqual(expectedTime.Day);
            actualTime.Month.ShouldEqual(expectedTime.Month);
            actualTime.Year.ShouldEqual(expectedTime.Year);
        }

        [Test]
        public void By_default_OutTime_should_be_One_Hour_greater_than_current_time()
        {
            AssertDateTimeEquality(game.OutTime, DateTime.UtcNow.ToIST().AddHours(1));
        }

        [Test]
        public void In_time_should_not_be_a_time_at_future()
        {
            game.InTime = DateTime.UtcNow.ToIST().AddHours(1);
            game.OutTime = DateTime.UtcNow.ToIST().AddHours(3);

            AssertEntityValidationError(game, "In Time should not be greater than current time.");
        }

        [Test]
        public void HoursPlayed_should_be_the_difference_of_InTime_and_OutTime()
        {
            game.HoursPlayed.ShouldEqual(1);

            game.InTime = DateTime.UtcNow.ToIST().AddMinutes(30);
            game.HoursPlayed.ShouldEqual(0.5);
        }

        [Test]
        public void PaymentType_should_Be_one_time_by_default()
        {
            game.GamePaymentType.ShouldEqual(GamePaymentType.OneTime);
        }
    }
}