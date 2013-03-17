using System.Web.Mvc;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
