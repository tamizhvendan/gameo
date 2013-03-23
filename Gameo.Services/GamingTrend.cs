using System;
using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;

namespace Gameo.Services
{
    public class GamingTrend : IGamingTrend
    {
        public IEnumerable<Bucket<Game>> Compute(IEnumerable<Game> games)
        {
            var bucketizer = new Bucketizer<Game>(games);
            
            TrendRules.Keys.ToList().ForEach(ruleLabel => bucketizer.AddRule(ruleLabel, TrendRules[ruleLabel]));
            
            return bucketizer.Bucketify();
        }

        private IDictionary<string, Func<Game, bool>> TrendRules
        {
            get
            {
                return new Dictionary<string, Func<Game, bool>>
                {
                    {"9-11", game => game.InTime.Hour >= 9 && game.InTime.Hour < 11},
                    {"11-13", game => game.InTime.Hour >= 11 && game.InTime.Hour < 13},
                    {"13-15", game => game.InTime.Hour >= 13 && game.InTime.Hour < 15},
                    {"15-17", game => game.InTime.Hour >= 15 && game.InTime.Hour < 17},
                    {"17-19", game => game.InTime.Hour >= 17 && game.InTime.Hour < 19},
                    {"19-22", game => game.InTime.Hour >= 19 && game.InTime.Hour < 22}
                };
            }
        } 
    }
}