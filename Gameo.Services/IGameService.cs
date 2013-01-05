using System;
using System.Collections.Generic;

namespace Gameo.Services
{
    public interface IGameService
    {
        IEnumerable<GameStatus> GetNonCompletedGamesStatus(string branchName, DateTime currentTime);
    }
}