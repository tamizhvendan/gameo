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
            AssertReadirectToAction(actionResult, "Index");
        }

        protected void AssertReadirectToAction(ActionResult actionResult, string actionName, string controllerName, string areaName)
        {
            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            AssertReadirectToAction(actionResult, actionName, controllerName);
            redirectToRouteResult.RouteValues["Area"].ShouldEqual(areaName);
        }

        protected void AssertReadirectToAction(ActionResult actionResult, string actionName, string controllerName)
        {
            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            AssertReadirectToAction(actionResult, actionName);
            redirectToRouteResult.RouteValues["Controller"].ShouldEqual(controllerName);
        }

        protected void AssertReadirectToAction(ActionResult actionResult, string actionName)
        {
            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            redirectToRouteResult.RouteValues["Action"].ShouldEqual(actionName);
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