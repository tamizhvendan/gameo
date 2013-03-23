using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.Services
{
    public interface IGamingTrend
    {
        IEnumerable<Bucket<Game>> Compute(IEnumerable<Game> games);
    }
}