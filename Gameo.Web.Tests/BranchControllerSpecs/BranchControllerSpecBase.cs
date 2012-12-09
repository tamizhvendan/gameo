using Gameo.DataAccess.Core;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.BranchControllerSpecs
{
    public abstract class BranchControllerSpecBase
    {
        protected Mock<IBranchRepository> BranchRepositoryMock;
        protected BranchController BranchController;

        [SetUp]
        public void BranchControllerSpecSetUp()
        {
            BranchRepositoryMock = new Mock<IBranchRepository>();
            BranchController = new BranchController(BranchRepositoryMock.Object);    
        }
    }
}