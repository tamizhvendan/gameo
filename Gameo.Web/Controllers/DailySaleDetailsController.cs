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

        public RedirectToRouteResult Index(CustomUserIdentity customUserIdentity)
        {
            if (!dailySaleDetailsRepository.IsDailySaleClosed(DateTime.Now.ToIST(), customUserIdentity.BranchName))
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("DailySaleClosed");
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
            dailySaleDetails.BranchName = customUserIdentity.BranchName;
            dailySaleDetailsRepository.Add(dailySaleDetails);
            return RedirectToAction("Index");
        }

        public ViewResult DailySaleClosed()
        {
            return View();
        }
    }
}