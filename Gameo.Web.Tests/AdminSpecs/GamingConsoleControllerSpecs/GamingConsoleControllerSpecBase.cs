using Gameo.DataAccess.Core;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.GamingConsoleControllerSpecs
{
    public abstract class GamingConsoleControllerSpecBase : ControllerSpecBase
    {
        protected GamingConsoleController GamingConsoleController;

        [SetUp]
        public void BranchControllerSpecSetUp()
        {
            GamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            GamingConsoleController = new GamingConsoleController(GamingConsoleRepositoryMock.Object, BranchRepositoryMock.Object);    
        }
    }
}