using System;
using Gameo.Domain;
using Gameo.Web.Models;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.DailySaleDetailsControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : DailySaleDetailsControllerSpecBase
    {
        private CustomUserIdentity customUserIdentity;

        [SetUp]
        public void SetUp()
        {
            customUserIdentity = new CustomUserIdentity(new User { BranchName = "branch1"});
        }

        [Test]
        public void Redirects_to_Create_View_if_DailySales_not_closed()
        {
            var dateTime = DateTime.Now.ToIST();
            DailySalesDetailRepositoryMock.Setup(repo => repo.IsDailySaleClosed(dateTime, customUserIdentity.BranchName)).Returns(false);

            var redirectToRouteResult = DailySaleDetailsController.Index(customUserIdentity);

            AssertReadirectToAction(redirectToRouteResult, "Create");
        }

        [Test]
        public void Redirect_to_DailySaleClosedView_if_DailySales_is_closed()
        {
            DailySalesDetailRepositoryMock.Setup(repo => repo.IsDailySaleClosed(It.IsAny<DateTime>(), customUserIdentity.BranchName)).Returns(true);

            var redirectToRouteResult = DailySaleDetailsController.Index(customUserIdentity);

            AssertReadirectToAction(redirectToRouteResult, "DailySaleClosed");
        }
    }
}