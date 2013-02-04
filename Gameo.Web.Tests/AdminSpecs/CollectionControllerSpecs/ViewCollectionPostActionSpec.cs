using System;
using Gameo.Domain;
using Gameo.Services;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.CollectionControllerSpecs
{
    [TestFixture]
    public class ViewCollectionPostActionSpec : CollectionControllerSpecBase
    {
        [Test]
        public void Returns_TotalCollection_retreived_from_collection_service()
        {
            var currentTime = DateTime.UtcNow.ToIST();
            const string branchName = "branch1";
            var totalCollection = new TotalDayCollection();
            CollectionServiceMock.Setup(service => service.GetTotalDayCollection(branchName, currentTime)).Returns(totalCollection);

            var jsonResult = CollectionController.ViewCollection(branchName, currentTime);
            
            jsonResult.Data.ShouldEqual(totalCollection);
        }
    }
}