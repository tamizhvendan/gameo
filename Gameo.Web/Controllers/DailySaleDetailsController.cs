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

        public RedirectToRouteResult Index()
        {
            if (!dailySaleDetailsRepository.IsDailySaleClosed(DateTime.Now))
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("DailySaleClosed");
        }

        public ViewResult Create()
        {
            return View(new DailySaleDetails());
        }

        public ActionResult Create(DailySaleDetails dailySaleDetails, CustomUserIdentity customUserIdentity)
        {
            if (!ModelState.IsValid)
            {
                return View(dailySaleDetails);
            }
            dailySaleDetails.BranchName = customUserIdentity.BranchName;
            dailySaleDetailsRepository.Add(dailySaleDetails);
            return RedirectToAction("Index");
        }
    }
}