using System;
using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.Models;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.DailySaleDetailsControllerSpecs
{
    [TestFixture]
    public class CreatePostActionSpec : DailySaleDetailsControllerSpecBase
    {
        private CustomUserIdentity customUserIdentity;
        private DailySaleDetails dailySaleDetails;
        private string password;

        [SetUp]
        public void SetUp()
        {
            dailySaleDetails = new DailySaleDetails();
            password = "password";
            customUserIdentity = new CustomUserIdentity(new User {BranchName = "Branch1", Password = password });
            DailySalesDetailRepositoryMock.Setup(repo => repo.IsDailySaleClosed(It.IsAny<DateTime>(), It.IsAny<string>())).Returns(false);
        }

        [Test]
        public void Returns_Create_View_If_Model_State_Is_Invalid()
        {
            DailySaleDetailsController.ModelState.AddModelError("foo", "bar");

            var viewResult = DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, password) as ViewResult;

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(dailySaleDetails);
            
        }

        [Test]
        public void Returns_Create_View_With_Model_Error_If_Password_is_Mismatching()
        {
            var viewResult = DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, "foo") as ViewResult;

            AssertModelError(DailySaleDetailsController, "Password", "Invalid Password");
            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(dailySaleDetails);
        }

        [Test]
        public void Returns_Create_View_with_Model_Error_if_DayOfSaleClosed_for_the_given_DateTime()
        {
            DailySalesDetailRepositoryMock.Setup(repo => repo.IsDailySaleClosed(dailySaleDetails.DateTime, customUserIdentity.BranchName)).Returns(true);

            var viewResult = DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, "password") as ViewResult;

            AssertModelError(DailySaleDetailsController, "DateTime", "Daily sale already closed for the given day!");
            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(dailySaleDetails);
        }

        [Test]
        public void If_model_state_is_valid_updates_BranchName_and_Add_using_DailySaleDetails_repository()
        {
            var dailySaleDetailsUpdated = dailySaleDetails;
            dailySaleDetailsUpdated.BranchName = customUserIdentity.BranchName;
            DailySalesDetailRepositoryMock.Setup(repo => repo.Add(dailySaleDetailsUpdated)).Verifiable();

            DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, password);

            DailySalesDetailRepositoryMock.Verify(repo => repo.Add(dailySaleDetailsUpdated));
        }

        [Test]
        public void Upon_successful_addition_redirects_to_create_view()
        {
            var redirectToRouteResult = DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, password) as RedirectToRouteResult;

            AssertReadirectToAction(redirectToRouteResult, "Index", "Game");
        }
    }
}