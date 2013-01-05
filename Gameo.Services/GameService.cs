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

        public GameService(IGameRepository gameRepository, IGamingConsoleRepository gamingConsoleRepository)
        {
            this.gameRepository = gameRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
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
    }
}