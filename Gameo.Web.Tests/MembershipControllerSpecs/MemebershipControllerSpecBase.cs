using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Controllers;
using Gameo.Web.Models;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.MembershipControllerSpecs
{
    public abstract class MemebershipControllerSpecBase : ControllerSpecBase
    {
        protected MembershipController MembershipController;
        protected Mock<IMembershipRepository> MembershipRepositoryMock;
        
        [SetUp]
        public void MemebershipControllerSpecSetUp()
        {
            User = new User {BranchName = "foo"};
            CustomUserIdentity = new CustomUserIdentity(User);
            MembershipRepositoryMock = new Mock<IMembershipRepository>();
            GamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            MembershipController = new MembershipController(MembershipRepositoryMock.Object, GamingConsoleRepositoryMock.Object);
        }
    }
}
