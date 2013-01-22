using System.Web.Mvc;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.BranchControllerSpecs
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
        public void Returns_create_view_if_new_branch_is_invalid()
        {
            BranchController.ModelState.AddModelError("Name", "The Name field is required");
            var invalidBranch = new Branch();

            var viewResult = BranchController.Create(invalidBranch) as ViewResult;

            viewResult.Model.ShouldEqual(invalidBranch);
            viewResult.ViewName.ShouldEqual(string.Empty);
        }

        [Test]
        public void Returs_crete_view_with_model_error_if_new_branch_name_already_exists()
        {
            var branchWithExistingBranchName = new Branch() {Name = "foo"};
            BranchRepositoryMock.Setup(repo => repo.IsBranchNameExists(branchWithExistingBranchName.Name)).Returns(true);

            var viewResult = BranchController.Create(branchWithExistingBranchName) as ViewResult;

            viewResult.Model.ShouldEqual(branchWithExistingBranchName);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(BranchController, "Name", "Branch name already exists");
        }

        [Test]
        public void Upon_successful_adding_redirect_to_the_Branch_index_page()
        {
            var actionResult = BranchController.Create(new Branch());

            AssertReadirectToIndexAction(actionResult);
        }
    }
}