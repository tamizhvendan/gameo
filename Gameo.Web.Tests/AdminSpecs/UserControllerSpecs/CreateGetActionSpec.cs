using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.UserControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : UserControllerSpecBase
    {
        [Test]
        public void Returns_create_view_with_User_model()
        {
            var viewResult = UserController.Create();

            viewResult.ViewName.ShouldEqual(string.Empty);
            var user = viewResult.Model as User;
            user.Id.ShouldEqual(Guid.Empty);
            user.Name.ShouldBeNull();
        }

        [Test]
        public void Retrieves_list_of_branches_and_pass_it_to_view()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = UserController.Create();

            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}