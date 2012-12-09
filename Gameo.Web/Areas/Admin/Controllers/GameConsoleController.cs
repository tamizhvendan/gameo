using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Web.Areas.Admin.Controllers
{
    public class GameConsoleController : Controller
    {
        private readonly IGameConsoleRepository gameConsoleRepository;
        private readonly IBranchRepository branchRepository;

        public GameConsoleController(IGameConsoleRepository gameConsoleRepository, IBranchRepository branchRepository)
        {
            this.gameConsoleRepository = gameConsoleRepository;
            this.branchRepository = branchRepository;
        }

        public ViewResult Index()
        {
            var gameConsoles = gameConsoleRepository.All.ToList();
            
            return View(gameConsoles);
        }


        public ViewResult Create()
        {
            var gameConsole = new GameConsole();
            ViewBag.Branches = branchRepository.All.Select(branch => new SelectListItem
                                                                         {
                                                                             Text = branch.Name,
                                                                             Value = branch.Name
                                                                         });
            return View(gameConsole);
        }

        [HttpPost]
        public ActionResult Create(GameConsole gameConsole)
        {
            gameConsoleRepository.Add(gameConsole);
            return RedirectToAction("Index");
        }
    }
}
