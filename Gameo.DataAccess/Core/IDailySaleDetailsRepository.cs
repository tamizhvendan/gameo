using System;
using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IDailySaleDetailsRepository : IRepository<DailySaleDetails>
    {
        bool IsDailySaleClosed(DateTime dateTime, string branchName);
    }
}