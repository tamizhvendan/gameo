using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.Areas.Admin.Controllers;
using Gameo.Web.Tests.BranchControllerSpecs;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : GameConsoleControllerSpecBase
    {
        [Test]
        public void Returns_create_view_with_branch_model()
        {
            var viewResult = GameConsoleController.Create();

            viewResult.ViewName.ShouldEqual(string.Empty);
            var gameConsole = viewResult.Model as GameConsole;
            gameConsole.Id.ShouldEqual(Guid.Empty);
            gameConsole.Name.ShouldBeNull();
        } 

        [Test]
        public void Retrieves_list_of_branches_and_pass_it_to_view()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = GameConsoleController.Create();

            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}