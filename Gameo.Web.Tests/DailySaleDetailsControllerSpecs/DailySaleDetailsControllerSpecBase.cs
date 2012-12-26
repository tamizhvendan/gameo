using Gameo.DataAccess.Core;
using Gameo.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.DailySaleDetailsControllerSpecs
{
    public abstract class DailySaleDetailsControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IDailySaleDetailsRepository> DailySalesDetailRepositoryMock;
        protected DailySaleDetailsController DailySaleDetailsController;

        [SetUp]
        public void DailySaleDetailsControllerSpecInit()
        {
            DailySalesDetailRepositoryMock = new Mock<IDailySaleDetailsRepository>();
            DailySaleDetailsController = new DailySaleDetailsController(DailySalesDetailRepositoryMock.Object);
        }
    }
}