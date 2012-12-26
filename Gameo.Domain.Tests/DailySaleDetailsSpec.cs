using System;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class DailySaleDetailsSpec : EntitySpecBase
    {
        private DailySaleDetails dailySaleDetails;

        [SetUp]
        public void SetUp()
        {
            dailySaleDetails = new DailySaleDetails { EbMeterReading = 5, TotalCollection = 5 };
        }

        [Test]
        public void DateTime_should_be_current_Day_by_default()
        {
            var currentTime = DateTime.Now;

            dailySaleDetails.DateTime.Day.ShouldEqual(currentTime.Day);
            dailySaleDetails.DateTime.Month.ShouldEqual(currentTime.Month);
            dailySaleDetails.DateTime.Year.ShouldEqual(currentTime.Year);
            dailySaleDetails.DateTime.Hour.ShouldEqual(0);
            dailySaleDetails.DateTime.Minute.ShouldEqual(0);
        }

        [Test]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        [TestCase(-60, false)]
        [TestCase(60, true)]
        [TestCase(1, true)]
        public void EbMeterReading_should_be_greater_than_zero(decimal ebMeterReading, bool isHappyPath)
        {
            dailySaleDetails.EbMeterReading = ebMeterReading;
            if (isHappyPath)
            {
                AssertZeroValidationError(dailySaleDetails);
            }
            else
            {
                AssertEntityValidationError(dailySaleDetails, "EB Meter Reading should be greater than zero.");
            }

        }

        [Test]
        [TestCase(-1, false)]
        [TestCase(-5, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        public void TotalCollection_should_be_zero_or_more(decimal totalCollection, bool isHappyPath)
        {
            dailySaleDetails.TotalCollection = totalCollection;
            if (isHappyPath)
            {
                AssertZeroValidationError(dailySaleDetails);
            }
            else
            {
                AssertEntityValidationError(dailySaleDetails, "Total Collection should be greater than or equal to zero.");
            }
        }
    }
}