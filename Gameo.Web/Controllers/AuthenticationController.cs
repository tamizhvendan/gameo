using System.Web.Mvc;
using Gameo.Web.ViewModels;

namespace Gameo.Web.Controllers
{
    public class AuthenticationController : Controller
    {

        public ViewResult Login()
        {
            var loginViewModel = new LoginViewModel
                                     {
                                        UserName = "", Password = "", 
                                        BranchSelectListItems = new[]
                                        {
                                            new SelectListItem{ Selected = true, Text = "Select a branch", Value = "0" },
                                            new SelectListItem {Selected = true, Text = "T.Nagar", Value = "1"}, 
                                            new SelectListItem {Text = "Virugambakam", Value = "2"}
                                        }
                                     };
            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            return View(loginViewModel);
        }
    }
}
