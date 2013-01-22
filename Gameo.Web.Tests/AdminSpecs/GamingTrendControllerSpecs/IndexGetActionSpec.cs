using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.GamingTrendControllerSpecs
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

        [Test]
        public void Retrieves_Braches_From_BranchRepository_And_Put_Them_Into_ViewBag()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = GamingTrendController.Index();
            
            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}