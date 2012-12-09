using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameConsoleControllerSpecs
{
    public abstract class GameConsoleControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IGameConsoleRepository> GameConsoleRepositoryMock;
        protected Mock<IBranchRepository> BranchRepositoryMock;
        protected GameConsoleController GameConsoleController;

        [SetUp]
        public void BranchControllerSpecSetUp()
        {
            BranchRepositoryMock = new Mock<IBranchRepository>();
            GameConsoleRepositoryMock = new Mock<IGameConsoleRepository>();
            GameConsoleController = new GameConsoleController(GameConsoleRepositoryMock.Object, BranchRepositoryMock.Object);    
        }

        protected void SetupBranchRepositoryToReturnSomeRandomBranches()
        {
            var branches = new[] { new Branch { Name = "foo" }, new Branch { Name = "bar" } };
            BranchRepositoryMock.Setup(repo => repo.All).Returns(branches);
        }

        protected void AssertRandomBranchesPresentInViewBag(ViewResult viewResult)
        {
            var actualBranches = viewResult.ViewBag.Branches as IEnumerable<SelectListItem>;
            actualBranches.Count().ShouldEqual(2);
            actualBranches.Any(item => item.Text == "foo" && item.Value == "foo").ShouldBeTrue();
            actualBranches.Any(item => item.Text == "bar" && item.Value == "bar").ShouldBeTrue();
        }
    }
}