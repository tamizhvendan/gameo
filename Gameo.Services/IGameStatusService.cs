using System;
using System.Collections.Generic;

namespace Gameo.Services
{
    public interface IGameStatusService
    {
        IEnumerable<GameStatus> GetNonCompletedGameStatuses(string branchName, DateTime currentTime);
    }
}