using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.DailySaleDetailsControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : DailySaleDetailsControllerSpecBase
    {
        [Test]
        public void Returns_Create_View_With_DailySalesDetails_ViewModel()
        {
            var viewResult = DailySaleDetailsController.Create();

            viewResult.Model.ShouldBeType<DailySaleDetails>();
            viewResult.Model.ShouldNotBeNull();
            viewResult.ViewName.ShouldEqual(string.Empty);
        }
    }
}