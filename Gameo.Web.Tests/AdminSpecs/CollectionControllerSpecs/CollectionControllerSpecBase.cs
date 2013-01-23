using Gameo.Services;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.CollectionControllerSpecs
{
    [TestFixture]
    public class CollectionControllerSpecBase : ControllerSpecBase
    {
        protected Mock<ICollectionService> CollectionServiceMock;
        protected CollectionController CollectionController;

        [SetUp]
        public void CollectionControllerSpecSetUp()
        {
            CollectionServiceMock = new Mock<ICollectionService>();
            CollectionController = new CollectionController(CollectionServiceMock.Object, BranchRepositoryMock.Object);
        }
    }
}