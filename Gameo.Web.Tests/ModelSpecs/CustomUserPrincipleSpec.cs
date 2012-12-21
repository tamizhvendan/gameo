using Gameo.Domain;
using Gameo.Web.Models;
using Should;
using NUnit.Framework;

namespace Gameo.Web.Tests.ModelSpecs
{
    [TestFixture]
    public class CustomUserPrincipleSpec
    {
        private User user;
        private CustomUserIdentity customUserIdentity;
        private CustomUserPrinciple customUserPrinciple;

        [SetUp]
        public void SetUp()
        {
            user = new User();
            customUserIdentity = new CustomUserIdentity(user);
            customUserPrinciple = new CustomUserPrinciple(customUserIdentity);
        }

        [Test]
        public void Returns_Custom_User_Identity()
        {
            customUserPrinciple.Identity.ShouldEqual(customUserIdentity);
        }

        [Test]
        public void IsInRole_for_admin_role_returns_true_if_the_user_is_admin()
        {
            user.IsAdmin = true;

            customUserPrinciple.IsInRole("admin").ShouldBeTrue();
            customUserPrinciple.IsInRole("ADmin").ShouldBeTrue();
        }

        [Test]
        public void IsInRole_for_admin_role_returns_false_if_the_user_is_non_admin()
        {
            user.IsAdmin = false;

            customUserPrinciple.IsInRole("admin").ShouldBeFalse();
        }

        [Test]
        public void IsInRole_for_user_role_returns_true_if_the_user_is_non_admin()
        {
            user.IsAdmin = false;

            customUserPrinciple.IsInRole("user").ShouldBeTrue();
            customUserPrinciple.IsInRole("usER").ShouldBeTrue();
        }

        [Test]
        public void IsInRole_for_user_role_returns_false_if_the_user_is_admin()
        {
            user.IsAdmin = false;

            customUserPrinciple.IsInRole("admin").ShouldBeFalse();
        }

        [Test]
        public void IsInRole_returns_false_for_any_other_roles()
        {
            customUserPrinciple.IsInRole("foo").ShouldBeFalse();
            customUserPrinciple.IsInRole("bar").ShouldBeFalse();
            customUserPrinciple.IsInRole("foobar").ShouldBeFalse();
        }
    }
}