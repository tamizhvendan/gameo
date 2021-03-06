using System;
using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.Services
{
    public interface IGameService
    {
        IEnumerable<GameStatus> GetNonCompletedGamesStatus(string branchName, DateTime currentTime);
        void AssignConsoleForMembership(Membership membership, Game game);
        void MarkGameAsInvalid(Guid id);
        IEnumerable<Bucket<Game>> GetGamingTrends(TrendRequest trendRequest);
        IEnumerable<GamePriceTrend> GetGamePriceTrends(IEnumerable<Bucket<Game>> buckets);
    }
}