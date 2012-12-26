using System;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class DailySaleDetailsSpec : RepositorySpecBase<DailySaleDetails>
    {
        private DailySaleDetailsRepository dailySaleDetailsRepository;

        [SetUp]
        public void SetUp()
        {
            dailySaleDetailsRepository = new DailySaleDetailsRepository();
        }

        [Test]
        public void IsDailySaleClosed_returs_true_if_the_daily_sale_Details_Exists_for_given_day()
        {
            var dateTime = DateTime.Now;
            AddEntityToDatabase(new DailySaleDetails());

            var isDailySaleClosed = dailySaleDetailsRepository.IsDailySaleClosed(dateTime);

            isDailySaleClosed.ShouldBeTrue();
        }

        [Test]
        public void IsDailySaleClosed_returs_false_if_the_daily_sale_Details_not_Exists_for_given_day()
        {
            var dateTime = DateTime.Now.AddDays(1);
            AddEntityToDatabase(new DailySaleDetails());

            var isDailySaleClosed = dailySaleDetailsRepository.IsDailySaleClosed(dateTime);

            isDailySaleClosed.ShouldBeFalse();
        }
    }
}