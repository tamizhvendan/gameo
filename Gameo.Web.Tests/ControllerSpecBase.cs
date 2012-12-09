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
    }
}