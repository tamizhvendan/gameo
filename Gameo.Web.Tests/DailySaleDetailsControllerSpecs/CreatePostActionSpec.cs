using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.Models;
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
        public void Returs_Create_View_With_Model_Error_If_Password_is_Mismatching()
        {
            var viewResult = DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, "foo") as ViewResult;

            AssertModelError(DailySaleDetailsController, "Password", "Invalid Password");
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
        public void Upon_successful_addition_redirects_to_index_view()
        {
            var actionResult = DailySaleDetailsController.Create(dailySaleDetails, customUserIdentity, password);

            AssertReadirectToIndexAction(actionResult);
        }
    }
}