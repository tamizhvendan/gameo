using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public IEnumerable<Game> GetNonCompletedGames(string gamingConsoleName, DateTime currentTime)
        {
            return EntityCollection
                .AsQueryable()
                .Where(game => game.ConsoleName == gamingConsoleName)
                .Where(game => game.OutTime > currentTime);
        }

        public IEnumerable<Game> GetCompletedGamesWithinGivenDay(string gamingConsoleName, DateTime dateTime)
        {
            return EntityCollection
                .AsQueryable()
                .Where(game => game.OutTime < dateTime)
                .Where(game => game.ConsoleName == gamingConsoleName)
                .Where(game => game.InTime > DateTime.Today);
        }
    }
}