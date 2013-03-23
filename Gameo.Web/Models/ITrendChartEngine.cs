using System.Collections.Generic;
using Gameo.Domain;
using Gameo.Services;

namespace Gameo.Web.Models
{
    public interface ITrendChartEngine
    {
        IEnumerable<TrendChartDatum> Transform(IEnumerable<Bucket<Game>> gameBuckets);
    }
}