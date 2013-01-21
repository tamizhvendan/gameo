using System.Web.Mvc;
using Gameo.Domain;
using Gameo.Web.Areas.Admin.Controllers;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.UserControllerSpecs
{
    public class CreatePostActionSpec : UserControllerSpecBase
    {
        [Test]
        public void Adds_new_User_to_the_User_repository()
        {
            var user = new User();
            UserRepositoryMock.Setup(repo => repo.Add(user)).Verifiable();

            UserController.Create(user);

            UserRepositoryMock.Verify(repo => repo.Add(user));
        }

        [Test]
        public void Upon_successful_adding_redirect_to_the_GamingConsole_index_page()
        {
            var actionResult = UserController.Create(new User());

            AssertReadirectToIndexAction(actionResult);
        }

        [Test]
        public void Returns_create_view_if_new_User_is_invalid()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            UserController.ModelState.AddModelError("Name", "The Name field is required");

            var user = new User();

            var viewResult = UserController.Create(user) as ViewResult;

            viewResult.Model.ShouldEqual(user);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void Returns_create_view_if_new_user_name_already_exists()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            var user = new User { Name = "foo", Password = "foobar", BranchName = "bar" };
            UserRepositoryMock.Setup(repo => repo.IsUserNameExists("foo", "bar")).Returns(true);

            var viewResult = UserController.Create(user) as ViewResult;

            viewResult.Model.ShouldEqual(user);
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(UserController, "Name", "User name already exists in the selected branch");
            AssertRandomBranchesPresentInViewBag(viewResult);
        }
    }
}