using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.DailySaleDetailsControllerSpecs
{
    [TestFixture]
    public class DailySaleClosedGetActionSpec : DailySaleDetailsControllerSpecBase
    {
        [Test]
        public void Returns_DailySaleClosed_View()
        {
            var viewResult = DailySaleDetailsController.DailySaleClosed();

            viewResult.ViewName.ShouldEqual(string.Empty);
        }
    }
}