using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class UserRepositorySpec : RepositoryTestBase<User>
    {
        private UserRepository userRepository;

        [SetUp]
        public void SetUp()
        {
            userRepository = new UserRepository();
        }

        [Test]
        public void Adds_new_User()
        {
            var user = CreateUser("foo", "bar");

            userRepository.Add(user);

            AssertNewlyAddedEntity(actualUserStored =>
            {
                actualUserStored.Name.ShouldEqual(user.Name);
                actualUserStored.Password.ShouldEqual(user.Password);
            });
        }

        [Test]
        public void Checks_the_existance_of_user_name_within_the_branch_with_case_ignored()
        {
            var user1 = CreateUser("user1", "", "branch1");
            var user2 = CreateUser("user2", "", "branch2");

            AddEntityToDatabase(user1, user2);

            var isUser1Exists = userRepository.IsUserNameExists("user1".ToUpperInvariant(), "Branch1".ToUpperInvariant());
            var isUser2Exists = userRepository.IsUserNameExists("user2".ToUpperInvariant(), "Branch2".ToUpperInvariant());

            isUser1Exists.ShouldBeTrue();
            isUser2Exists.ShouldBeTrue();
        }

        private User CreateUser(string userName, string password, string branchName = "")
        {
            return new User {Name = userName, Password = password, BranchName = branchName};
        }

        [Test]
        public void Deactivates_the_user_account()
        {
            var userToDeActivate = CreateUser("user1", "password1", "branch1");
            AddEntityToDatabase(userToDeActivate);

            userRepository.DeActivateUser(userToDeActivate.Id);

            AssertUpdatedEntity(userToDeActivate.Id, user => user.IsActive.ShouldBeFalse());
        }

        [Test]
        public void Activates_the_user_account()
        {
            var userToActivate = CreateUser("user1", "password1", "branch1");
            userToActivate.IsActive = false;
            AddEntityToDatabase(userToActivate);

            userRepository.ActivateUser(userToActivate.Id);

            AssertUpdatedEntity(userToActivate.Id, user => user.IsActive.ShouldBeTrue());
        }

        [Test]
        public void Retrieves_user_by_username_with_case_ignored()
        {
            var user = CreateUser("user1", "password1");
            AddEntityToDatabase(user);

            var retrievedUser = userRepository.GetByUserName("uSEr1");

            retrievedUser.Password.ShouldEqual(user.Password);
            retrievedUser.Name.ShouldEqual(user.Name);
            retrievedUser.Id.ShouldEqual(user.Id);
        }

        [Test]
        public void Throws_Exception_if_username_not_exists_when_retriving_by_name()
        {
            var user = CreateUser("user1", "password1");
            AddEntityToDatabase(user);

            var argumentException = Assert.Throws<ArgumentException>(() => userRepository.GetByUserName("user2"));
            argumentException.Message.ShouldEqual("Username not exists");
        }
    }
}