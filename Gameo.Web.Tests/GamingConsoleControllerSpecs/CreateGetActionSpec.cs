using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GamingConsoleControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : GamingConsoleControllerSpecBase
    {
        [Test]
        public void Returns_create_view_with_GamingConsole_model()
        {
            var viewResult = GamingConsoleController.Create();

            viewResult.ViewName.ShouldEqual(string.Empty);
            var gamingConsole = viewResult.Model as GamingConsole;
            gamingConsole.Id.ShouldEqual(Guid.Empty);
            gamingConsole.Name.ShouldBeNull();
        } 

        [Test]
        public void Retrieves_list_of_branches_and_pass_it_to_view()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = GamingConsoleController.Create();

            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}