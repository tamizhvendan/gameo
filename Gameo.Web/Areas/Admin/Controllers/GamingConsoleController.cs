using System;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class GamingConsoleController : ApplicationControllerBase
    {
        private readonly IGamingConsoleRepository gamingConsoleRepository;
        private readonly IBranchRepository branchRepository;

        public GamingConsoleController(IGamingConsoleRepository gamingConsoleRepository, IBranchRepository branchRepository)
        {
            this.gamingConsoleRepository = gamingConsoleRepository;
            this.branchRepository = branchRepository;
        }

        public ViewResult Index()
        {
            var gameConsoles = gamingConsoleRepository.All.ToList();
            
            return View(gameConsoles);
        }


        public ViewResult Create()
        {
            var gameConsole = new GamingConsole();
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View(gameConsole);
        }

        [HttpPost]
        public ActionResult Create(GamingConsole gamingConsole)
        {
            if (ModelState.IsValid)
            {
                if (!gamingConsoleRepository.IsConsoleNameExists(gamingConsole.Name, gamingConsole.BranchName))
                {
                    gamingConsoleRepository.Add(gamingConsole);
                    return RedirectToAction("Index");   
                }
                ModelState.AddModelError("name", "Console name already exists in the selected branch");
            }

            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View(gamingConsole);
        }

        public ViewResult Edit(Guid id)
        {
            var gameConsole = gamingConsoleRepository.GetById(id);
            
            return View(gameConsole);
        }

        [HttpPost]
        public ActionResult Edit(GamingConsole gamingConsole)
        {
            gamingConsoleRepository.Update(gamingConsole);

            return RedirectToAction("Index");
        }
    }
}
