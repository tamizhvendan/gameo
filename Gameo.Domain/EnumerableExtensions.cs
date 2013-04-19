using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameo.Domain
{
    public static class EnumerableExtensions
    {
        public static void ForEachWithIndex<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }

        public static IEnumerable<T2> SelectWithIndex<T1,T2>(this IEnumerable<T1> ie, Func<T1, int,T2> func)
        {
            var i = 0;
            return ie.Select(e => func(e, i++));
        }
    }
}