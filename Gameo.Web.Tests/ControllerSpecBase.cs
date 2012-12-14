using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests
{
    public abstract class ControllerSpecBase
    {
        protected Mock<IBranchRepository> BranchRepositoryMock;

        [SetUp]
        public void ControllerSpecSetUp()
        {
            BranchRepositoryMock = new Mock<IBranchRepository>();
        }


        protected void AssertReadirectToIndexAction(ActionResult actionResult)
        {
            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            redirectToRouteResult.RouteValues["Action"].ShouldEqual("Index");
        }

        protected void AssertModelError(Controller controller, string propertyName, string errorMessage)
        {
            controller.ModelState.IsValid.ShouldBeFalse();
            controller.ModelState.Values.Count.ShouldEqual(1);
            var modelState = controller.ModelState[propertyName];
            modelState.Errors.Count.ShouldEqual(1);
            modelState.Errors.First().ErrorMessage.ShouldEqual(errorMessage);
        }

        protected void SetupBranchRepositoryToReturnSomeRandomBranches()
        {
            var branches = new[] { new Branch { Name = "foo" }, new Branch { Name = "bar" } };
            BranchRepositoryMock.Setup(repo => repo.All).Returns(branches);
        }

        protected void AssertRandomBranchesPresentInViewBag(ViewResult viewResult)
        {
            var actualBranches = viewResult.ViewBag.Branches as IEnumerable<SelectListItem>;
            actualBranches.Count().ShouldEqual(2);
            actualBranches.Any(item => item.Text == "foo" && item.Value == "foo").ShouldBeTrue();
            actualBranches.Any(item => item.Text == "bar" && item.Value == "bar").ShouldBeTrue();
        }

    }
}