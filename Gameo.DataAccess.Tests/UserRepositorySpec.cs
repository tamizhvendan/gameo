using Gameo.DataAccess.Core;
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
            var user = new User {Name = "foo", Password = "bar"};

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
            var user1 = new User { Name = "user1", BranchName = "branch1" }; 
            var user2 = new User { Name = "user2", BranchName = "branch2" };

            AddEntityToDatabase(user1, user2);

            var isUser1Exists = userRepository.IsUserNameExists("user1".ToUpperInvariant(), "Branch1".ToUpperInvariant());
            var isUser2Exists = userRepository.IsUserNameExists("Console2".ToUpperInvariant(), "Branch2".ToUpperInvariant());

            isUser1Exists.ShouldBeTrue();
            isUser2Exists.ShouldBeTrue();
        }
    }
}