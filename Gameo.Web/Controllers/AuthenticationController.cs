using System;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.Models;
using Gameo.Web.ViewModels;

namespace Gameo.Web.Controllers
{
    public class AuthenticationController : ApplicationControllerBase
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
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                var loggedUser = authenticationService.Authenticate(loginViewModel.UserName, loginViewModel.Password, loginViewModel.BranchName);
                authenticationService.SetAuthCookie(loginViewModel.UserName);
                HttpContext.User = new CustomUserPrinciple(new CustomUserIdentity(loggedUser));
                if (loggedUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });    
                }

                return RedirectToAction("Index", "Game");
            }
            catch (Exception exception)
            {
                ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
                ModelState.AddModelError("UserName", exception.Message);
                return View(loginViewModel);
            }
        }

        public RedirectToRouteResult LogOff()
        {
            authenticationService.LogOff();

            return RedirectToAction("Login");
        }
    }
}
