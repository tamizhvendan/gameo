using System.Web.Mvc;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : ApplicationControllerBase
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}