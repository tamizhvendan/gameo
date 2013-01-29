using System;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CollectionController : ApplicationControllerBase
    {
        private readonly ICollectionService collectionService;
        private readonly IBranchRepository branchRepository;

        public CollectionController(ICollectionService collectionService, IBranchRepository branchRepository)
        {
            this.collectionService = collectionService;
            this.branchRepository = branchRepository;
        }

        public ViewResult Index()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository); 
            return View();
        }

        public JsonResult ViewCollection(string branchName, DateTime day)
        {
            return Json(collectionService.GetTotalDayCollection(branchName, day));
        }
    }
}