using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.CollectionControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : CollectionControllerSpecBase
    {
        [Test]
        public void Returns_Index_View()
        {
            var viewResult = CollectionController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void Retrieves_Braches_From_BranchRepository_And_Put_Them_Into_ViewBag()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = CollectionController.Index();

            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}