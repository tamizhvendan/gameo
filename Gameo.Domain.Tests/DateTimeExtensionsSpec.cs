using System;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class DateTimeExtensionsSpec
    {
        [Test]
        public void Can_get_first_day_of_the_DateTime()
        {
            var dateTime = DateTime.Now;
            var expectedDateTime = new DateTime(dateTime.Year, dateTime.Month, 1);

            var firstDayOfMonth = dateTime.FirstDayOfMonth();

            firstDayOfMonth.ShouldEqual(expectedDateTime);
        }

        [Test]
        [TestCase(2013,3,31)]
        [TestCase(2013,2,28)]
        [TestCase(2012,2,29)]
        [TestCase(2013,4,30)]
        public void Can_get_last_day_of_the_DateTime(int year, int month, int expectedLastDay)
        {
            var dateTime = new DateTime(year, month, 1);
            var expectedDateTime = new DateTime(year, month, expectedLastDay);

            var lastDayOfMonth = dateTime.LastDayOfMonth();

            lastDayOfMonth.ShouldEqual(expectedDateTime);
        }

        [Test]
        public void Can_get_last_seven_months()
        {
            var dateTime = new DateTime(1998, 1, 15);

            var lastSevenMonths = dateTime.LastSevenMonths().ToList();

            lastSevenMonths.Count.ShouldEqual(7);
            lastSevenMonths[0].ShouldEqual(new DateTime(1998,1,1));
            for (int i = 1; i < lastSevenMonths.Count; i++)
            {
                lastSevenMonths[i].ShouldEqual(new DateTime(1997, (13-i), 1));
            }
        }
    }
}