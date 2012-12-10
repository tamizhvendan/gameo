using System;
using System.Collections.Generic;
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
            ViewBag.Branches = MapBranchesToSelectListItems();
            return View(gameConsole);
        }

        private IEnumerable<SelectListItem> MapBranchesToSelectListItems()
        {
            return branchRepository
                    .All
                    .Select(branch => new SelectListItem
                    {
                        Text = branch.Name,
                        Value = branch.Name
                    });
        }

        [HttpPost]
        public ActionResult Create(GameConsole gameConsole)
        {
            if (ModelState.IsValid)
            {
                if (!gameConsoleRepository.IsConsoleNameExists(gameConsole.Name, gameConsole.BranchName))
                {
                    gameConsoleRepository.Add(gameConsole);
                    return RedirectToAction("Index");   
                }
                ModelState.AddModelError("Name", "Console Name already exists in the selected branch");
            }

            ViewBag.Branches = MapBranchesToSelectListItems();
            return View(gameConsole);
        }

        public ViewResult Edit(Guid id)
        {
            var gameConsole = gameConsoleRepository.GetById(id);
            
            return View(gameConsole);
        }

        [HttpPost]
        public ActionResult Edit(GameConsole gameConsole)
        {
            gameConsoleRepository.Update(gameConsole);

            return RedirectToAction("Index");
        }
    }
}
