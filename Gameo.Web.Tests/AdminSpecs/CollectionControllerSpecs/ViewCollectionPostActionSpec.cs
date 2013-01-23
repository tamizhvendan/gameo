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
            var currentTime = DateTime.Now.ToIST();
            const string branchName = "branch1";
            var totalCollection = new TotalCollection();
            CollectionServiceMock.Setup(service => service.GetTotalCollection(branchName, currentTime)).Returns(totalCollection);

            var jsonResult = CollectionController.ViewCollection(branchName, currentTime);
            
            jsonResult.Data.ShouldEqual(totalCollection);
        }
    }
}