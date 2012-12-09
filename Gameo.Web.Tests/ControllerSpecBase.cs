using System.Linq;
using System.Web.Mvc;
using Should;

namespace Gameo.Web.Tests
{
    public abstract class ControllerSpecBase
    {
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
    }
}