using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using Gameo.Services;

namespace Gameo.Web.Models
{
    public class TrendChartEngine : ITrendChartEngine
    {
        private readonly Dictionary<string, IList<int>> gamingConsoleHash = new Dictionary<string, IList<int> >();
 
        public IEnumerable<TrendChartDatum> Transform(IEnumerable<Bucket<Game>> gameBuckets)
        {
            var buckets = gameBuckets.ToList();

            buckets
                .Select(bucket => bucket.Values)
                .ToList()
                .ForEach(games => games.ToList().ForEach(game => CreateHashForEachGamingConsole(game, buckets.Count)));

            for (int i = 0; i < buckets.Count; i++)
            {
                UpdateGamingConsoleHashWithBucketValues(buckets[i], i);
            }

            return gamingConsoleHash.Keys.Select(consoleName => new TrendChartDatum {Name = consoleName, Data = gamingConsoleHash[consoleName]});
        }

        private void UpdateGamingConsoleHashWithBucketValues(Bucket<Game> gameBucket, int dataIndex)
        {
            gameBucket
                .Values
                .ToList()
                .ForEach(game =>
                {
                    var gamingConsoleData = gamingConsoleHash[game.ConsoleName];
                    gamingConsoleData[dataIndex]++;
                });
        }

        private void CreateHashForEachGamingConsole(Game game, int bucketCount)
        {
            if (gamingConsoleHash.ContainsKey(game.ConsoleName)) 
                return;
            
            var dataForGamingConsole = new List<int>();
            for (var i = 0; i < bucketCount; i++)
            {
                dataForGamingConsole.Add(0);
            }
            gamingConsoleHash.Add(game.ConsoleName, dataForGamingConsole);
        }
    }
}