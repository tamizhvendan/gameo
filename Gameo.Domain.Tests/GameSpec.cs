using System;
using NUnit.Framework;

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
                           ConsoleName = "Console1", CustomerName = "Customer1",  InTime = DateTime.Now, OutTime = DateTime.Now.AddHours(1), Price = 10
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
        public void Out_time_is_required()
        {
            game.OutTime = DateTime.MinValue;

            AssertEntityValidationError(game, "Out Time is required.");
        }

        [Test]
        public void Out_time_should_be_greater_than_in_time()
        {
            game.InTime = DateTime.Now;
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
            game.InTime = DateTime.Now;
            game.OutTime = DateTime.Now.AddMinutes(noOfMinutesToAdd);

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
        [TestCase(0, false)]
        [TestCase(10, true)]
        [TestCase(-4, false)]
        [TestCase(-1, false)]
        public void Price_should_be_greater_than_zero(decimal price, bool isHappyPath = false)
        {
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
    }
}