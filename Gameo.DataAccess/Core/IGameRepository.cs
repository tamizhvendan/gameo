﻿using System;
using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetNonCompletedGames(string branchName, DateTime currentTime);
        IEnumerable<Game> GetCompletedGamesWithinGivenDay(string branchName, DateTime dateTime);
        IEnumerable<Game> GetGames(string branchName, DateTime from, DateTime to);
        IEnumerable<Game> GetGames(string branchName, DateTime gamesPlayedOn);
    }
}