using System;
using NUnit.Framework;

namespace Gameo.Web.Tests.UserControllerSpecs
{
    [TestFixture]
    public class DeActivateActionSpec : UserControllerSpecBase
    {
        [Test]
        public void Deactivates_user_using_user_repository()
        {
            var newGuid = Guid.NewGuid();
            UserRepositoryMock.Setup(repo => repo.DeActivateUser(newGuid)).Verifiable();

            UserController.DeActivate(newGuid);

            UserRepositoryMock.Verify(repo => repo.DeActivateUser(newGuid));
        }

        [Test]
        public void Upon_successful_deactivation_redirect_to_index_page()
        {
            var redirectToRouteResult = UserController.DeActivate(Guid.NewGuid());

            AssertReadirectToIndexAction(redirectToRouteResult);
        }
    }
}