using Gameo.Web.Areas.Admin.Controllers;
using NUnit.Framework;

namespace Gameo.Web.Tests.GamingTrendControllerSpecs
{
    public abstract class GamingTrendControllerSpecBase : ControllerSpecBase
    {
        protected GamingTrendController GamingTrendController;

        [SetUp]
        public void GamingTrendControllerSpecInit()
        {
            GamingTrendController = new GamingTrendController();
        }
    }
}