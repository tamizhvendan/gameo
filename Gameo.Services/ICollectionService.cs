using System;

namespace Gameo.Services
{
    public interface ICollectionService
    {
        TotalDayCollection GetTotalDayCollection(string branchName, DateTime dateTime);
    }
}