using System;
using System.Web.Security;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class AuthenticationServiceSpec
    {
        private AuthenticationService authenticationService;
        private Mock<IUserRepository> userRepositoryMock;
        private string userName;
        private string password;
        private string branchName;

        [SetUp]
        public void SetUp()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            authenticationService = new AuthenticationService(userRepositoryMock.Object);
            userName = "user1";
            password = "password";
            branchName = "branch1";
        }

        [Test]
        public void Returns_user_instance_upon_successful_authentication()
        {
            var expectedUser = new User {BranchName = branchName, Name = userName, Password = password};
            userRepositoryMock.Setup(repo => repo.GetByUserName(userName)).Returns(expectedUser);

            var actualUser = authenticationService.Authenticate(userName, password, branchName);

            actualUser.ShouldEqual(expectedUser);
        }

        [Test]
        [TestCase("passWord")]
        [TestCase("password1")]
        public void Throws_Exception_if_password_mismatch(string samplePassword)
        {
            var expectedUser = new User { BranchName = branchName, Name = userName, Password = password };
            userRepositoryMock.Setup(repo => repo.GetByUserName(userName)).Returns(expectedUser);

            var argumentException = Assert.Throws<ArgumentException>(() => authenticationService.Authenticate(userName, samplePassword, branchName));
            argumentException.Message.ShouldEqual("Invalid User Credentials");
        }

        [Test]
        [TestCase("brabch11")]
        [TestCase("branCH")]
        public void Throws_Exception_if_branchname_mismatch(string sampleBranchName)
        {
            var expectedUser = new User { BranchName = branchName, Name = userName, Password = password };
            userRepositoryMock.Setup(repo => repo.GetByUserName(userName)).Returns(expectedUser);

            var argumentException = Assert.Throws<ArgumentException>(() => authenticationService.Authenticate(userName, password, sampleBranchName));
            argumentException.Message.ShouldEqual("Invalid User Credentials");
        }

        [Test]
        public void Throws_Exception_if_username_not_exists()
        {
            var expectedArgumentException = new ArgumentException("Username not exists");
            userRepositoryMock.Setup(repo => repo.GetByUserName(userName)).Throws(expectedArgumentException);

            var actualArgumentException = Assert.Throws<ArgumentException>(() => authenticationService.Authenticate(userName, password, branchName));

            actualArgumentException.ShouldEqual(expectedArgumentException);
        }

        [Test]
        public void Dont_verify_branchName_if_the_authenticated_user_is_a_admin()
        {
            var expectedUser = new User { BranchName = branchName, Name = userName, Password = password , IsAdmin = true};
            userRepositoryMock.Setup(repo => repo.GetByUserName(userName)).Returns(expectedUser);

            var actualUser = authenticationService.Authenticate(userName, password, "foo");

            actualUser.ShouldEqual(expectedUser);
        }
    }
}