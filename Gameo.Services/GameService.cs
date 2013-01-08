using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;
        private readonly IMembershipRepository membershipRepository;

        public GameService(IGameRepository gameRepository, IGamingConsoleRepository gamingConsoleRepository, IMembershipRepository membershipRepository)
        {
            this.gameRepository = gameRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
            this.membershipRepository = membershipRepository;
        }

        public IEnumerable<GameStatus> GetNonCompletedGamesStatus(string branchName, DateTime currentTime)
        {
            var gamingConsoles = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            var nonCompletedGames = gameRepository.GetNonCompletedGames(branchName, currentTime).ToList();
            foreach (var gamingConsole in gamingConsoles)
            {
                var gameStatus = new GameStatus(gamingConsole.Name);
                foreach (var nonCompletedGame in nonCompletedGames.Where(game => game.ConsoleName == gameStatus.GamingConsoleName))
                {
                    gameStatus
                        .AddPlayer(new Player
                                       {
                                           CustomerName = nonCompletedGame.CustomerName , 
                                           InTime = nonCompletedGame.InTime, OutTime = nonCompletedGame.OutTime
                                       });
                }
                yield return gameStatus;
            }
        }

        public void AssignConsoleForMembership(Membership membership, Game game)
        {
            gameRepository.Add(game);
            membership.AddGame(game);
            membershipRepository.Update(membership);
        }
    }
}