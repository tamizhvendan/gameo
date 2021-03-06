﻿using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.GameControllerSpecs
{
    [TestFixture]
    public class RemoveCustomerPostActionSpec : GameControllerSpecBase
    {
        [Test]
        public void Retrieves_GamingConsoles_of_user_branch_from_GamingConsole_Repository_and_put_them_in_ViewBag()
        {
            Games.Add(new Game());
            SetUpRepositoryWithGamingConsoles();

            var viewResult = GameController.RemoveCustomer(Games, CustomUserIdentity);

            AssertGamingConsolesInViewBag(viewResult);
        }

        [Test]
        public void Removes_last_game_instance_in_the_games_list_pass_it_as_view_model()
        {
            var firstGame = new Game();
            var secondGame = new Game();
            Games.Add(firstGame);
            Games.Add(secondGame);

            var viewResult = GameController.RemoveCustomer(Games, CustomUserIdentity);

            var actualGames = viewResult.Model as List<Game>;
            actualGames.Count.ShouldEqual(1);
            actualGames.First().ShouldEqual(firstGame);
        }

        [Test]
        public void Returns_Assign_Console_View()
        {
            Games.Add(new Game());
            var viewResult = GameController.RemoveCustomer(Games, CustomUserIdentity);

            viewResult.ViewName.ShouldEqual("AssignConsole");
        }
    }
}