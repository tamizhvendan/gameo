using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Models;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class GameController : ApplicationControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;

        public GameController(IGameRepository gameRepository, IGamingConsoleRepository gamingConsoleRepository)
        {
            this.gameRepository = gameRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ViewResult AssignConsole(CustomUserIdentity customUserIdentity)
        {
            RetrieveGamingConsolesAndPutItInViewBag(customUserIdentity.BranchName);
            var games = new List<Game>();
            games.Add(new Game());
            return View(games);
        }

        [HttpPost]
        [HttpParamAction]
        public ActionResult AssignConsole(List<Game> games, CustomUserIdentity userIdentity)
        {
            if (ModelState.IsValid)
            {
                gameRepository.AddMany(games);
                return RedirectToAction("Index");
            }
            RetrieveGamingConsolesAndPutItInViewBag(userIdentity.BranchName);
            return View("AssignConsole", games);
        }

        [HttpPost]
        [HttpParamAction]
        public ViewResult AddCustomer(List<Game> games, CustomUserIdentity userIdentity)
        {
            RetrieveGamingConsolesAndPutItInViewBag(userIdentity.BranchName);

            if (ModelState.IsValid)
            {
                games.Add(new Game());
                return View("AssignConsole",games);
            }
            return View("AssignConsole",games);
        }

        private void RetrieveGamingConsolesAndPutItInViewBag(string branchName)
        {
            var gamingConsolesByBranchName = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            ViewBag.GamingConsoles =
                gamingConsolesByBranchName.Select(console => new SelectListItem {Text = console.Name, Value = console.Name});
        }

        [HttpParamAction]
        [HttpPost]
        public ViewResult RemoveCustomer(List<Game> games, CustomUserIdentity customUserIdentity)
        {
            RetrieveGamingConsolesAndPutItInViewBag(customUserIdentity.BranchName);
            var lastAddedGame = games.Last();
            games.Remove(lastAddedGame);
            return View("AssignConsole", games);
        }
    }
}
