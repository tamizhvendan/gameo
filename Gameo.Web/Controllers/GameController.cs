using System.Web.Mvc;
using Gameo.Web.Models;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class GameController : ApplicationControllerBase
    {
        public ActionResult Index(CustomUserIdentity customUserIdentity)
        {
            return View();
        }

    }
}
