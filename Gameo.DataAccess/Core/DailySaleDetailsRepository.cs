using System;
using System.Linq;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess.Core
{
    public class DailySaleDetailsRepository : RepositoryBase<DailySaleDetails>, IDailySaleDetailsRepository
    {
        public bool IsDailySaleClosed(DateTime dateTime, string branchName)
        {
            return GetDailySaleDetails(branchName, dateTime) != null;
        }

        public DailySaleDetails GetDailySaleDetails(string branchName, DateTime dateTime)
        {
            var givenDay = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            
            var currentSaleDetails = EntityCollection.AsQueryable()
                                            .FirstOrDefault(dailySaleDetails => dailySaleDetails.DateTime == givenDay &&
                                                                                        dailySaleDetails.BranchName == branchName);

            return currentSaleDetails;
        }

        public decimal GetEbMeterReadingForTheMonth(string branchName, int year, int month)
        {
            var startDateTime = new DateTime(year, month, 1);
            var endDateTime = startDateTime.LastDayOfMonth();

            var salesDetailForGivenMonth = EntityCollection
                .AsQueryable()
                .Where(dailySaleDetailsRepository => dailySaleDetailsRepository.DateTime >= startDateTime && dailySaleDetailsRepository.DateTime <= endDateTime).ToList();

            if (!salesDetailForGivenMonth.Any())
            {
                return 0;
            }

            var startingReadingForTheMonth = salesDetailForGivenMonth.Min(dailySaleDetails => dailySaleDetails.EbMeterReading);
            var endingReadingForTheMonth = salesDetailForGivenMonth.Max(dailySaleDetails => dailySaleDetails.EbMeterReading);

            return endingReadingForTheMonth - startingReadingForTheMonth;
        }
    }
}