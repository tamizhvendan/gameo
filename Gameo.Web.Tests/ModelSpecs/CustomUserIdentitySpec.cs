using Gameo.Domain;
using Gameo.Web.Models;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.ModelSpecs
{
    [TestFixture]
    public class CustomUserIdentitySpec
    {
        private User user;
        private CustomUserIdentity customUserIdentity;

        [SetUp]
        public void SetUp()
        {
            user = new User();
            customUserIdentity = new CustomUserIdentity(user);
        }

        [Test]
        public void Retrieves_User_Name()
        {
            user.Name = "foo";

            customUserIdentity.Name.ShouldEqual("foo");
        }

        [Test]
        public void Retreives_Authentication_Type()
        {
            customUserIdentity.AuthenticationType.ShouldEqual("custom");
        }

        [Test]
        public void IsAuthenticated_returs_true_if_user_name_exists()
        {
            user.Name = "foo";

            customUserIdentity.IsAuthenticated.ShouldBeTrue();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void IsAuthenticated_returs_false_if_user_name_not_exists(string userName)
        {
            user.Name = userName;

            customUserIdentity.IsAuthenticated.ShouldBeFalse();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsAdmin_returns_admin_status_of_the_user(bool isAdmin)
        {
            user.IsAdmin = isAdmin;

            customUserIdentity.IsAdmin.ShouldEqual(user.IsAdmin);
        }

        [Test]
        public void BranchName_returns_branch_name_of_the_user()
        {
            user.BranchName = "foo";

            customUserIdentity.BranchName.ShouldEqual(user.BranchName);
        }

        [Test]
        public void Password_Returns_the_password_of_the_user()
        {
            user.Password = "foo";

            customUserIdentity.Password.ShouldEqual(user.Password);
        }
    }
}