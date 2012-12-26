using System;
using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.Services
{
    public interface IGameService
    {
        IEnumerable<GameStatus> GetNonCompletedGamesStatus(string branchName, DateTime currentTime);
        IEnumerable<Game> GetNonCompletedGames(string branchName, DateTime currentTime);
        IEnumerable<Game> GetCompletedGamesWithinGivenDay(string branchName, DateTime currentTime);
    }
}