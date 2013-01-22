using System;

namespace Gameo.Services
{
    public interface ICollectionService
    {
        TotalCollection GetTotalCollection(string branchName, DateTime dateTime);
    }
}