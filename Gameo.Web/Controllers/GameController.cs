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
        private readonly IGamingConsoleRepository gamingConsoleRepository;

        public GameController(IGamingConsoleRepository gamingConsoleRepository)
        {
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
            for (var i = 0; i < 6; i++)
            {
                games.Add(new Game());
            }
            return View(games);
        }

        private void RetrieveGamingConsolesAndPutItInViewBag(string branchName)
        {
            var gamingConsolesByBranchName = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            ViewBag.GamingConsoles =
                gamingConsolesByBranchName.Select(console => new SelectListItem {Text = console.Name, Value = console.Name});
        }
    }
}
