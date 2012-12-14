using Gameo.Web.Areas.Admin.Controllers;
using NUnit.Framework;

namespace Gameo.Web.Tests.BranchControllerSpecs
{
    public abstract class BranchControllerSpecBase : ControllerSpecBase
    {
        protected BranchController BranchController;

        [SetUp]
        public void BranchControllerSpecSetUp()
        {
            BranchController = new BranchController(BranchRepositoryMock.Object);    
        }
    }
}