using System;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.AdminControllerSpecs
{
    [TestFixture]
    public class ViewGamesPostActionSpec : AdminControllerSpecBase
    {
        [Test]
        public void Retrieve_Games_from_Game_repository()
        {
            var gamesPlayedOn = DateTime.Now;
            var branchName = "branch";
            var games = Enumerable.Empty<Game>();
            GameRepositoryMock.Setup(repo => repo.GetGames(branchName, gamesPlayedOn)).Returns(games);

            var actualGames = AdminController.ViewGames(branchName, gamesPlayedOn).Data;

            actualGames.ShouldEqual(games);
        }
    }
}