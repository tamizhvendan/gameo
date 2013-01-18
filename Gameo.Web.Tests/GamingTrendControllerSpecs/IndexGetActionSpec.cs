using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GamingTrendControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : GamingTrendControllerSpecBase
    {
        [Test]
        public void Returns_Index_View()
        {
            var viewResult = GamingTrendController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
        }
    }
}