﻿using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class UserSpec : EntitySpecBase
    {
        [Test]
        public void UserName_should_not_be_empty()
        {
            var user = new User {Password = "foo"};

            AssertEntityValidationError(user, "The Name field is required.");
        }

        [Test]
        public void Password_should_not_be_empty()
        {
            var user = new User {Name = "foo"};

            AssertEntityValidationError(user, "The Password field is required.");
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void BranchName_should_not_be_empty_if_the_user_is_not_an_adminstrator(string branchName)
        {
            var user = new User {Name = "foo", IsAdmin = false, Password = "bar", BranchName = branchName};

            AssertEntityValidationError(user, "Branch Name field is required for a non-admin user");
        }

        [Test]
        public void IsActive_should_be_true_by_default()
        {
            var user = new User();

            user.IsActive.ShouldBeTrue();
        }
    }
}