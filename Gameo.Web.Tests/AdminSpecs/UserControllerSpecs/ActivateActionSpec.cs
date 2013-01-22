using System;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.UserControllerSpecs
{
    [TestFixture]
    public class ActivateActionSpec : UserControllerSpecBase
    {
        [Test]
        public void Activates_user_using_user_repository()
        {
            var newGuid = Guid.NewGuid();
            UserRepositoryMock.Setup(repo => repo.ActivateUser(newGuid)).Verifiable();

            UserController.Activate(newGuid);

            UserRepositoryMock.Verify(repo => repo.ActivateUser(newGuid));
        }

        [Test]
        public void Upon_successful_deactivation_redirect_to_index_page()
        {
            var redirectToRouteResult = UserController.Activate(Guid.NewGuid());

            AssertReadirectToIndexAction(redirectToRouteResult);
        }
    }
}