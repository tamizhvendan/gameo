using System.Web.Mvc;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.BranchControllerSpecs
{
    [TestFixture]
    public class CreatePostActionSpec : BranchControllerSpecBase
    {
        [Test]
        public void Adds_new_branch_to_the_branch_repository()
        {
            var branch = new Branch();
            BranchRepositoryMock.Setup(repo => repo.Add(branch)).Verifiable();

            BranchController.Create(branch);

            BranchRepositoryMock.Verify(repo => repo.Add(branch), Times.Once());
        }

        [Test]
        public void Upon_successful_adding_redirect_to_the_Branch_index_page()
        {
            var actionResult = BranchController.Create(new Branch());

            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            redirectToRouteResult.RouteValues["Action"].ShouldEqual("Index");
        }
    }
}