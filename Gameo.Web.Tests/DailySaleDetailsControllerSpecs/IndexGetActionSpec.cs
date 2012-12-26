using System;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.DailySaleDetailsControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : DailySaleDetailsControllerSpecBase
    {
        [Test]
        public void Redirects_to_Create_View_if_DailySales_not_closed()
        {
            var dateTime = DateTime.Now;
            DailySalesDetailRepositoryMock.Setup(repo => repo.IsDailySaleClosed(dateTime)).Returns(false);

            var redirectToRouteResult = DailySaleDetailsController.Index();

            AssertReadirectToAction(redirectToRouteResult, "Create");
        }

        [Test]
        public void Redirect_to_DailySaleClosedView_if_DailySales_is_closed()
        {
            DailySalesDetailRepositoryMock.Setup(repo => repo.IsDailySaleClosed(It.IsAny<DateTime>())).Returns(true);

            var redirectToRouteResult = DailySaleDetailsController.Index();

            AssertReadirectToAction(redirectToRouteResult, "DailySaleClosed");
        }
    }
}