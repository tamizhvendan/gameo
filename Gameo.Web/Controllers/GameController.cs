﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Models;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class GameController : ApplicationControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;
        private readonly IGameStatusService gameStatusService;

        public GameController(IGameRepository gameRepository, IGamingConsoleRepository gamingConsoleRepository, IGameStatusService gameStatusService)
        {
            this.gameRepository = gameRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
            this.gameStatusService = gameStatusService;
        }

        public ViewResult Index(CustomUserIdentity customUserIdentity)
        {
            var nonCompletedGameStatuses = gameStatusService.GetNonCompletedGameStatuses(customUserIdentity.BranchName, DateTime.Now);
            return View(nonCompletedGameStatuses);
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
