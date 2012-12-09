using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.BranchControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : BranchControllerSpecBase
    {
        [Test]
        public void Returns_create_view_with_branch_model()
        {
            var viewResult = BranchController.Create();

            viewResult.ViewName.ShouldEqual(string.Empty);
            var branch = viewResult.Model as Branch;
            branch.Id.ShouldEqual(Guid.Empty);
            branch.Name.ShouldBeNull();
        }
    }
}