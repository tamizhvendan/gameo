using System;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Models;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class DailySaleDetailsController : ApplicationControllerBase
    {
        private readonly IDailySaleDetailsRepository dailySaleDetailsRepository;

        public DailySaleDetailsController(IDailySaleDetailsRepository dailySaleDetailsRepository)
        {
            this.dailySaleDetailsRepository = dailySaleDetailsRepository;
        }

        public ViewResult Create()
        {
            return View(new DailySaleDetails());
        }

        [HttpPost]
        public ActionResult Create(DailySaleDetails dailySaleDetails, CustomUserIdentity customUserIdentity, string password)
        {
            if (!ModelState.IsValid)
            {
                return View(dailySaleDetails);
            }
            if (password != customUserIdentity.Password)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View(dailySaleDetails);
            }
            if (dailySaleDetailsRepository.IsDailySaleClosed(dailySaleDetails.DateTime, customUserIdentity.BranchName))
            {
                ModelState.AddModelError("DateTime", "Daily sale already closed for the given day!");
                return View(dailySaleDetails);
            }
            dailySaleDetails.BranchName = customUserIdentity.BranchName;
            dailySaleDetailsRepository.Add(dailySaleDetails);
            return RedirectToAction("Index", "Game");
        }

        public ViewResult DailySaleClosed()
        {
            return View();
        }
    }
}