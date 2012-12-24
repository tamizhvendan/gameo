using System;
using System.Collections.Generic;
using Gameo.DataAccess.Core;

namespace Gameo.Services
{
    public class GameStatusService : IGameStatusService
    {
        private readonly IGameRepository gameRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;

        public GameStatusService(IGameRepository gameRepository, IGamingConsoleRepository gamingConsoleRepository)
        {
            this.gameRepository = gameRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
        }

        public IEnumerable<GameStatus> GetNonCompletedGameStatuses(string branchName, DateTime currentTime)
        {
            var gamingConsoles = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            foreach (var gamingConsole in gamingConsoles)
            {
                var gameStatus = new GameStatus(gamingConsole.Name);
                foreach (var nonCompletedGame in gameRepository.GetNonCompletedGames(gamingConsole.Name, currentTime))
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