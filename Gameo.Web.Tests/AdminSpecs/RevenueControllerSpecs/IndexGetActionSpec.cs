using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.RevenueControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : RevenueControllerSpecBase
    {
        [Test]
        public void Returns_Index_View()
        {
            var viewResult = RevenueController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void Retrieves_Braches_From_BranchRepository_And_Put_Them_Into_ViewBag()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = RevenueController.Index();
            
            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}