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
        protected CustomUserIdentity CustomUserIdentity;

        [SetUp]
        public void MemebershipControllerSpecSetUp()
        {
            CustomUserIdentity = new CustomUserIdentity(new User { BranchName = "foo"});
            MembershipRepositoryMock = new Mock<IMembershipRepository>();
            MembershipController = new MembershipController(MembershipRepositoryMock.Object);
        }
    }
}
