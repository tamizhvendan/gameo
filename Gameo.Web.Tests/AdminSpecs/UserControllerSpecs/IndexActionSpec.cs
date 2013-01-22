using System.Linq;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.UserControllerSpecs
{
    [TestFixture]
    public class IndexActionSpec : UserControllerSpecBase
    {
        [Test]
        public void Retrieve_all_Users_from_Users_repository()
        {
            UserRepositoryMock.Setup(repo => repo.All).Returns(Enumerable.Empty<User>()).Verifiable();

            UserController.Index();

            UserRepositoryMock.Verify(repo => repo.All, Times.Once());
        }

        [Test]
        public void Returns_index_view_with_list_of_users()
        {
            var users = Enumerable.Empty<User>();
            UserRepositoryMock.Setup(repo => repo.All).Returns(users);

            var viewResult = UserController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(users);
        }
    }
}