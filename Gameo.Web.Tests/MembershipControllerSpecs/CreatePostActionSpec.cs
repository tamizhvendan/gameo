using System.Web.Mvc;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    [TestFixture]
    public class CreatePostActionSpec : MemebershipControllerSpecBase
    {
        private Membership membership;

        [SetUp]
        public void SetUp()
        {
            membership = new Membership();
        }

        [Test]
        public void Return_Create_View_with_viewmodel_if_model_state_is_invalid()
        {
            MembershipController.ModelState.AddModelError("foo", "bar");

            var viewResult = MembershipController.Create(membership);

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(membership);
        }

        [Test]
        public void Add_Membership_to_the_repository_if_model_state_is_valid()
        {
            MembershipRepositoryMock.Setup(repo => repo.Add(membership)).Verifiable();

            MembershipController.Create(membership);

            MembershipRepositoryMock.Verify(repo => repo.Add(membership));
        }

        [Test]
        public void After_adding_returns_membership_created_view_with_membership_as_view_model()
        {
            var viewResult = MembershipController.Create(membership);

            viewResult.ViewName.ShouldEqual("MembershipCreated");
            viewResult.Model.ShouldEqual(membership);
        }
    }
}