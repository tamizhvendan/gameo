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
            var dateTime = DateTime.UtcNow.ToIST();
            AddEntityToDatabase(new DailySaleDetails { BranchName = "foo"});

            var isDailySaleClosed = dailySaleDetailsRepository.IsDailySaleClosed(dateTime, "foo");

            isDailySaleClosed.ShouldBeTrue();
        }

        [Test]
        public void IsDailySaleClosed_returs_false_if_the_daily_sale_Details_not_Exists_for_given_day()
        {
            var dateTime = DateTime.UtcNow.ToIST().AddDays(1);
            AddEntityToDatabase(new DailySaleDetails { BranchName = "bar"});

            var isDailySaleClosed = dailySaleDetailsRepository.IsDailySaleClosed(dateTime, "bar");

            isDailySaleClosed.ShouldBeFalse();
        }

        [Test]
        public void GetDailySaleDetails_return_DailySaleDetails_if_DailySaleDetails_exists_for_given_day()
        {
            AddEntityToDatabase(new DailySaleDetails { BranchName = "bar", EbMeterReading = 246, TotalCollection = 350});

            var dailySaleDetails = dailySaleDetailsRepository.GetDailySaleDetails("bar", DateTime.UtcNow.ToIST());

            dailySaleDetails.TotalCollection.ShouldEqual(350);
            dailySaleDetails.EbMeterReading.ShouldEqual(246);
        }

        [Test]
        public void GetDailySaleDetails_return_null_if_DailySaleDetails_not_exists_for_given_day()
        {
            AddEntityToDatabase(new DailySaleDetails { BranchName = "bar", EbMeterReading = 246, TotalCollection = 350 });

            var dailySaleDetails = dailySaleDetailsRepository.GetDailySaleDetails("bar", DateTime.UtcNow.ToIST().AddDays(1));

            dailySaleDetails.ShouldBeNull();
        }

        [Test]
        public void GetEbMeterReadingForTheMonth_return_difference_between_max_and_min_EbMeterReading_for_the_given_month()
        {
            var dateTimeForTheMonthOfMarch2013 = new DateTime(2013, 3, 1);
            const string branchName = "bar";
            AddEntityToDatabase(new DailySaleDetails { BranchName = branchName, EbMeterReading = 140, DateTime = dateTimeForTheMonthOfMarch2013.Subtract(new TimeSpan(12,0,0,0))});
            AddEntityToDatabase(new DailySaleDetails { BranchName = branchName, EbMeterReading = 240, DateTime = dateTimeForTheMonthOfMarch2013.AddDays(2)});
            AddEntityToDatabase(new DailySaleDetails { BranchName = branchName, EbMeterReading = 346, DateTime = dateTimeForTheMonthOfMarch2013.AddDays(10)});
            AddEntityToDatabase(new DailySaleDetails { BranchName = branchName, EbMeterReading = 450, DateTime = dateTimeForTheMonthOfMarch2013.AddDays(12)});
            var dailySaleDetails = new DailySaleDetails {BranchName = branchName, EbMeterReading = 540, DateTime = dateTimeForTheMonthOfMarch2013.AddDays(24)};
            AddEntityToDatabase(dailySaleDetails);

            var ebMeterReadingForTheMonthOfMarch2013
                     = dailySaleDetailsRepository.GetEbMeterReadingForTheMonth(branchName, 2013, 3);

            ebMeterReadingForTheMonthOfMarch2013.ShouldEqual(300);
        }

         [Test]
         public void GetEbMeterReadingForTheMonth_return_zero_if_no_reading_available_for_the_given_month()
         {
             const string branchName = "bar";

             var ebMeterReadingForTheMonth = dailySaleDetailsRepository.GetEbMeterReadingForTheMonth(branchName, 2013, 1);

             ebMeterReadingForTheMonth.ShouldEqual(0);
         }
    }
}