using Gameo.Web.Areas.Admin.Controllers;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.AdminControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : AdminControllerSpecBase
    {
        [Test]
        public void Returns_Index_View()
        {
            var viewResult = AdminController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void Retrieves_Braches_From_BranchRepository_And_Put_Them_Into_ViewBag()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = AdminController.Index();

            AssertRandomBranchesPresentInViewBag(viewResult);
        } 
    }
}