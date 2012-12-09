using System.Linq;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.BranchControllerSpecs
{
    [TestFixture]
    public class IndexActionSpec : BranchControllerSpecBase
    {
        [Test]
        public void Retrieve_all_branches_from_branches_repository()
        {
            BranchRepositoryMock.Setup(repo => repo.All).Verifiable();

            BranchController.Index();

            BranchRepositoryMock.Verify(repo => repo.All, Times.Once());
        }

        [Test]
        public void Returns_index_view_with_list_of_branches()
        {
            var branches = Enumerable.Empty<Branch>();
            BranchRepositoryMock.Setup(repo => repo.All).Returns(branches);

            var viewResult = BranchController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(branches);
        }
    }
}