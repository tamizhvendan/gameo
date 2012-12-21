using System;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.ViewModels;

namespace Gameo.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IBranchRepository branchRepository;
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IBranchRepository branchRepository, IAuthenticationService authenticationService)
        {
            this.branchRepository = branchRepository;
            this.authenticationService = authenticationService;
        }

        public ViewResult Login()
        {
            PutBranchesInViewBag();
            return View(new LoginViewModel());
        }

        private void PutBranchesInViewBag()
        {
            ViewBag.Branches = branchRepository
                .All
                .Select(branch => new SelectListItem
                                      {
                                          Text = branch.Name,
                                          Value = branch.Name
                                      });
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                var loggedUser = authenticationService.Authenticate(loginViewModel.UserName, loginViewModel.Password, loginViewModel.BranchName);
                authenticationService.SetAuthCookie(loginViewModel.UserName);
                Session["logged_user"] = loggedUser;
                if (loggedUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                return RedirectToAction("Index", "Game");
            }
            catch (Exception exception)
            {
                PutBranchesInViewBag();
                ModelState.AddModelError("UserName", exception.Message);
                return View(loginViewModel);
            }
        }

        public RedirectToRouteResult LogOff()
        {
            authenticationService.LogOff();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
