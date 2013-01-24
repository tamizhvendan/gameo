using System;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class DailySaleDetailsRepositorySpec : RepositorySpecBase<DailySaleDetails>
    {
        private DailySaleDetailsRepository dailySaleDetailsRepository;

        [SetUp]
        public void SetUp()
        {
            dailySaleDetailsRepository = new DailySaleDetailsRepository();
        }

        [Test]
        public void IsDailySaleClosed_returs_true_if_the_daily_sale_Details_Exists_for_branch_on_given_day()
        {
            var dateTime = DateTime.Now.ToIST();
            AddEntityToDatabase(new DailySaleDetails { BranchName = "foo"});

            var isDailySaleClosed = dailySaleDetailsRepository.IsDailySaleClosed(dateTime, "foo");

            isDailySaleClosed.ShouldBeTrue();
        }

        [Test]
        public void IsDailySaleClosed_returs_false_if_the_daily_sale_Details_not_Exists_for_given_day()
        {
            var dateTime = DateTime.Now.ToIST().AddDays(1);
            AddEntityToDatabase(new DailySaleDetails { BranchName = "bar"});

            var isDailySaleClosed = dailySaleDetailsRepository.IsDailySaleClosed(dateTime, "bar");

            isDailySaleClosed.ShouldBeFalse();
        }

        [Test]
        public void GetDailySaleDetails_return_DailySaleDetails_if_DailySaleDetails_exists_for_given_day()
        {
            AddEntityToDatabase(new DailySaleDetails { BranchName = "bar", EbMeterReading = 246, TotalCollection = 350});

            var dailySaleDetails = dailySaleDetailsRepository.GetDailySaleDetails("bar", DateTime.Now.ToIST());

            dailySaleDetails.TotalCollection.ShouldEqual(350);
            dailySaleDetails.EbMeterReading.ShouldEqual(246);
        }

        [Test]
        public void GetDailySaleDetails_return_null_if_DailySaleDetails_not_exists_for_given_day()
        {
            AddEntityToDatabase(new DailySaleDetails { BranchName = "bar", EbMeterReading = 246, TotalCollection = 350 });

            var dailySaleDetails = dailySaleDetailsRepository.GetDailySaleDetails("bar", DateTime.Now.ToIST().AddDays(1));

            dailySaleDetails.ShouldBeNull();
        }
    }
}