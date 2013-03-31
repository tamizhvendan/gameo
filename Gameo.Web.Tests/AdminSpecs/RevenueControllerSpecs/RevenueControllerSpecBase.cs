using Gameo.Services;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.RevenueControllerSpecs
{
    public class RevenueControllerSpecBase : ControllerSpecBase
    {
        protected RevenueController RevenueController;
        protected Mock<IRevenueService> RevenueServiceMock;

        [SetUp]
        public void RevenueControllerSpecSetUp()
        {
            RevenueServiceMock =  new Mock<IRevenueService>();
            RevenueController = new RevenueController(BranchRepositoryMock.Object, RevenueServiceMock.Object);
        }
    }
}