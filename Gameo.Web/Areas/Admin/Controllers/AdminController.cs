using System;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Services;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : ApplicationControllerBase
    {
        private readonly IBranchRepository branchRepository;
        private readonly IGameService gameService;
        private readonly IGameRepository gameRepository;

        public AdminController(IBranchRepository branchRepository, IGameService gameService, IGameRepository gameRepository)
        {
            this.branchRepository = branchRepository;
            this.gameService = gameService;
            this.gameRepository = gameRepository;
        }

        public ViewResult Index()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository); 

            return View();
        }

        public JsonResult ViewGames(string branchName, DateTime day)
        {
            return Json(gameRepository.GetGames(branchName, day));
        }

        public JsonResult InvalidateGame(Guid id)
        {
            gameService.MarkGameAsInvalid(id);
            return Json(null);
        }
    }
}
