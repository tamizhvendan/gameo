using System.Web.Mvc;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : ApplicationControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Collection");
        }
    }
}