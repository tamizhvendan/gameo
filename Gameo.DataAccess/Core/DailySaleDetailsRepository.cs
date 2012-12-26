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
            var givenDay = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

            var currentSaleDetails = EntityCollection.AsQueryable()
                                            .FirstOrDefault(dailySaleDetails => dailySaleDetails.DateTime == givenDay && 
                                                                                        dailySaleDetails.BranchName == branchName);

            return currentSaleDetails != null;
        }
    }
}