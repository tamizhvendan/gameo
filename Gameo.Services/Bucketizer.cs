using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameo.Services
{
    public class Bucketizer<T> where T : class
    {
        private readonly IEnumerable<T> bucketContents;
        private readonly IDictionary<string, Func<T, bool>> bucketRules;

        public Bucketizer(IEnumerable<T> bucketContents)
        {
            this.bucketContents = bucketContents;
            bucketRules = new Dictionary<string, Func<T, bool>>();
        }

        public void AddRule(string bucketLabel, Func<T, bool> bucketRule)
        {
            bucketRules.Add(bucketLabel, bucketRule);
        }

        public IEnumerable<Bucket<T>> Bucketify()
        {
            return bucketRules.Select(rule => new Bucket<T> {Label = rule.Key, Values = bucketContents.Where(rule.Value)});
        }
    }
}