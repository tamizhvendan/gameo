using System;
using System.Collections.Generic;
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

        public IEnumerable<Game> GetNonCompletedGames(string branchName, DateTime currentTime)
        {
            var nonCompletedGames = new List<Game>();
            var gamingConsoles = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            foreach (var gamingConsole in gamingConsoles)
            {
                nonCompletedGames.AddRange(gameRepository.GetNonCompletedGames(gamingConsole.Name, currentTime));
            }
            return nonCompletedGames;
        }

        public IEnumerable<Game> GetCompletedGamesWithinGivenDay(string branchName, DateTime currentTime)
        {
            var nonCompletedGames = new List<Game>();
            var gamingConsoles = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            foreach (var gamingConsole in gamingConsoles)
            {
                nonCompletedGames.AddRange(gameRepository.GetCompletedGamesWithinGivenDay(gamingConsole.Name, currentTime));
            }
            return nonCompletedGames;
        }
    }
}