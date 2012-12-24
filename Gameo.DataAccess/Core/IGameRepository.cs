using System;
using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetNonCompletedGames(string gamingConsoleName, DateTime currentTime);
    }
}