using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Controllers;
using Gameo.Web.Models;
using Moq;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    public abstract class GameControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IGamingConsoleRepository> GamingConsoleRepositoryMock;
        protected GameController GameController;
        protected CustomUserIdentity CustomUserIdentity;
        protected User User;
        protected List<Game> games;
        protected Mock<IGameRepository> GameRepositoryMock;
        protected Mock<IGameStatusService> GameStatusServiceMock;

        [SetUp]
        public void GameControllerSpecSetUp()
        {
            GamingConsoleRepositoryMock = new Mock<IGamingConsoleRepository>();
            GameRepositoryMock = new Mock<IGameRepository>();
            GameStatusServiceMock = new Mock<IGameStatusService>();
            GameController = new GameController(GameRepositoryMock.Object, GamingConsoleRepositoryMock.Object, GameStatusServiceMock.Object);
            User = new User();
            CustomUserIdentity = new CustomUserIdentity(User);
            games = new List<Game>();
        }

        protected void SetUpRepositoryWithGamingConsoles()
        {
            User.BranchName = "Branch1";
            var gamingConsoles = new List<GamingConsole>
                                     {
                                         new GamingConsole
                                             {
                                                 Name = "Console1",
                                             }
                                     };
            GamingConsoleRepositoryMock.Setup(repo => repo.GetGamingConsolesByBranchName(User.BranchName))
                .Returns(gamingConsoles);
        }

        protected static void AssertGamingConsolesInViewBag(ViewResult viewResult)
        {
            var selectListItems = viewResult.ViewBag.GamingConsoles as IEnumerable<SelectListItem>;
            selectListItems.Count().ShouldEqual(1);
            selectListItems.First().Text.ShouldEqual("Console1");
            selectListItems.First().Value.ShouldEqual("Console1");
        }
    }
}