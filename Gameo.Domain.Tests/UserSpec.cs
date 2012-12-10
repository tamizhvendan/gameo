using NUnit.Framework;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class UserSpec : EntitySpecBase
    {
        [Test]
        public void UserName_should_not_be_empty()
        {
            var user = new User {Password = "foo"};

            Validate(user, "The Name field is required.");
        }

        [Test]
        public void Password_should_not_be_empty()
        {
            var user = new User {Name = "foo"};

            Validate(user, "The Password field is required.");
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void BranchName_should_not_be_empty_if_the_user_is_not_an_adminstrator(string branchName)
        {
            var user = new User {Name = "foo", IsAdministrator = false, Password = "bar", BranchName = branchName};

            Validate(user, "Branch Name field is required for a non-admin user");
        }
    }
}