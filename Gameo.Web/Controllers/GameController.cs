using System.Web.Mvc;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class GameController : ApplicationControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
