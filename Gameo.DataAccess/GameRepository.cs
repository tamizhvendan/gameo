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
        public IEnumerable<Game> GetNonCompletedGames(string branchName, DateTime currentTime)
        {
            return EntityCollection
                .AsQueryable()
                .Where(game => game.IsValid)
                .Where(game => game.BranchName == branchName)
                .Where(game => game.OutTime > currentTime);
        }

        public IEnumerable<Game> GetCompletedGamesWithinGivenDay(string branchName, DateTime dateTime)
        {
            return EntityCollection
                .AsQueryable()
                .Where(game => game.IsValid)
                .Where(game => game.OutTime < dateTime)
                .Where(game => game.BranchName == branchName)
                .Where(game => game.InTime > dateTime.Date);
        }

        public IEnumerable<Game> GetGames(string branchName, DateTime from, DateTime to)
        {
            return EntityCollection
                .AsQueryable()
                .Where(game => game.IsValid)
                .Where(game => game.BranchName == branchName)
                .Where(game => game.InTime >= from)
                .Where(game => game.OutTime <= to);
        }

        public IEnumerable<Game> GetGames(string branchName, DateTime gamesPlayedOn)
        {
            return EntityCollection
                .AsQueryable()
                .Where(game => game.BranchName == branchName)
                .Where(game => game.IsValid)
                .Where(game => game.InTime > gamesPlayedOn.Date)
                .Where(game => game.InTime < gamesPlayedOn.Date.AddDays(1));
        }
    }
}